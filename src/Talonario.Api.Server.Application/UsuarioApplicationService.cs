using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Enums;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        #region Private Fields

        private readonly IInfracaoRepository _infracaoRepository;
        private readonly IOrgaoAutuadorRepository _orgaoAutuadorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        #endregion Private Fields

        #region Public Constructors

        public UsuarioApplicationService(
            IUsuarioRepository usuarioRepository,
            IOrgaoAutuadorRepository orgaoAutuadorRepository,
            IInfracaoRepository infracaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _orgaoAutuadorRepository = orgaoAutuadorRepository;
            _infracaoRepository = infracaoRepository;
        }

        #endregion Public Constructors

        #region Private Methods

        private void ValidaLoginPorCredenciaisPayload(UsuarioLoginInputModel usuarioInput,
            bool? validarIdDispositivo = false)
        {
            if (String.IsNullOrEmpty(usuarioInput.CPF))
                throw new ArgumentException(paramName: nameof(usuarioInput.CPF), message: "Campo 'CPF' é obrigatório.");

            if (usuarioInput.CPF?.Length < 11)
                throw new ArgumentException(paramName: nameof(usuarioInput.Senha), message: "Campo 'CPF' deve ter no mínimo 11 dígitos.");

            if (String.IsNullOrEmpty(usuarioInput.Senha))
                throw new ArgumentException(paramName: nameof(usuarioInput.Senha), message: "Campo 'Senha' é obrigatório.");

            if (usuarioInput.Senha?.Length < 4)
                throw new ArgumentException(paramName: nameof(usuarioInput.Senha), message: "Campo 'Senha' deve ter no mínimo 4 dígitos.");

            if (validarIdDispositivo != null && validarIdDispositivo == true)
            {
                if (String.IsNullOrEmpty(usuarioInput.IdDispositivo))
                    throw new ArgumentException(paramName: nameof(usuarioInput.CPF), message: "Campo 'IdDispositivo' é obrigatório.");

                if (usuarioInput.IdDispositivo?.Length < 5)
                    throw new ArgumentException(paramName: nameof(usuarioInput.Senha), message: "Campo 'IdDispositivo' inválido.");
            }
        }

        #endregion Private Methods

        public async Task<IEnumerable<UsuarioViewModel>> LoginPorCredenciais(UsuarioLoginInputModel usuarioLoginInput)
        {
            usuarioLoginInput.CPF = usuarioLoginInput.CPF?.Replace(".", "").Replace("-", "");

            ValidaLoginPorCredenciaisPayload(usuarioLoginInput, true);

            #region USUÁRIO ÚNICO POR DISPOSITIVO

            var usuarioLogado = await _usuarioRepository.VerificaUsuarioLogado(usuarioLoginInput.CPF);
            if (usuarioLogado != null &&
                usuarioLogado.IdDispositivo.Trim().ToLower() != usuarioLoginInput.IdDispositivo.Trim().ToLower() &&
                usuarioLogado.DataAutenticacao > DateTime.Now.AddHours(-24))
            {
                throw new Exception("Usuário está logado em outro dispositivo.");
            }
            else if (usuarioLogado == null)
            {
                await _usuarioRepository.InsereUsuarioLogado(usuarioLoginInput.CPF, usuarioLoginInput.IdDispositivo);
            }
            else if (usuarioLogado != null)
            {
                await _usuarioRepository.AtualizaUsuarioLogado(usuarioLoginInput.CPF, usuarioLoginInput.IdDispositivo);
            }

            #endregion USUÁRIO ÚNICO POR DISPOSITIVO

            var usuariosModel = new List<UsuarioViewModel>();

            var usuarios = await _usuarioRepository.ObterPorCPF(usuarioLoginInput.CPF);
            if (usuarios.Count() == 0) return null;

            string senhaEncriptada = usuarios?.FirstOrDefault()?.Senha ?? "";
            bool verify = !String.IsNullOrEmpty(senhaEncriptada) && BCrypt.Net.BCrypt.Verify(usuarioLoginInput.Senha, senhaEncriptada);
            if (!verify) return null;

            string hashToken = new JwtHelper().GenerateJwtTokenByHours(usuarios.First(), 24);

            var codOrgaosEmpresas = usuarios?.Select(u => u.IdEmpresa).Distinct().ToList();
            var orgaosAutuadores = await _orgaoAutuadorRepository.ObterOrgaoAutuadorPorEmpresa(codOrgaosEmpresas);

            foreach (var usuario in usuarios.ToList())
            {
                string tipoOrgaoAutuador = orgaosAutuadores?.FirstOrDefault(o =>
                    o.CodigoOrgaoAutuador == usuario.IdEmpresa)?.TipoOrgaoAutuador;

                var talonarioDispositivo = await _infracaoRepository.ObterInfracaoUsuarioDispositivoPorIdDispositivo(
                    usuarioLoginInput.IdDispositivo);

                if (talonarioDispositivo == null)
                {
                    int idTalonarioDispositivo = await _infracaoRepository.InserirInfracaoUsuarioDispositivo(
                        usuarioLoginInput.IdDispositivo, "0");

                    talonarioDispositivo = new TalonarioDispositivoEntity(
                        idTalonarioDispositivo,
                        usuarioLoginInput.IdDispositivo,
                        "0");
                }

                usuario.IdTalonarioDispositivo = talonarioDispositivo.Id;
                usuario.IdDispositivo = talonarioDispositivo.IdDispositivo;
                usuario.Sequencia = talonarioDispositivo.Sequencia;
                usuario.Competencia = EnumHelper.GetEnumDescription(Enum.Parse<OrgaoAutuadorEnum>(tipoOrgaoAutuador));
                usuario.Token = hashToken;

                usuariosModel.Add(
                    UsuarioViewModelMapper.UsuarioMapper(usuario)
                );
            }

            return usuariosModel;
        }

        public async Task<bool> Logout(UsuarioLogout usuarioLogout)
        {
            return await _usuarioRepository.Logout(usuarioLogout.CPF, usuarioLogout.IdDispositivo);
        }

        public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
        {
            var usuariosModel = new List<UsuarioViewModel>();
            var usuarios = await _usuarioRepository.ObterTodos();

            var codOrgaosEmpresas = usuarios?.Select(u => u.IdEmpresa).Distinct().ToList();
            var orgaosAutuadores = await _orgaoAutuadorRepository.ObterOrgaoAutuadorPorEmpresa(codOrgaosEmpresas);

            //usuarios.ToList().ForEach(async usuario =>
            foreach (var usuario in usuarios)
            {
                string tipoOrgaoAutuador = orgaosAutuadores?.FirstOrDefault(o =>
                    o.CodigoOrgaoAutuador == usuario.IdEmpresa)?.TipoOrgaoAutuador;

                if (usuario.Matricula > 0)
                    usuario.MatriculaAgente = _usuarioRepository.RetornaMatriculaAgente(usuario.Matricula);

                if (!string.IsNullOrEmpty(tipoOrgaoAutuador))
                    usuario.Competencia = EnumHelper.GetEnumDescription(Enum.Parse<OrgaoAutuadorEnum>(tipoOrgaoAutuador));

                usuariosModel.Add(
                    UsuarioViewModelMapper.UsuarioMapper(usuario)
                );
            }

            return usuariosModel;
        }

        public async Task<bool> PodeAssinar(string matricula)
        {
            return await _usuarioRepository.PodeAssinar(matricula);
        }
    }
}