using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Extensions;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class PessoaApplicationService : IPessoaApplicationService
    {
        #region Private Fields

        private readonly IPessoaRepository _pessoaRepository;

        #endregion Private Fields

        #region Public Constructors

        public PessoaApplicationService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> InserirPessoaAbordada(PessoaAbordadaViewModel pessoaAbordadaModel)
        {
            if (string.IsNullOrEmpty(pessoaAbordadaModel.IdPessoa))
            {
                throw new ArgumentException(paramName: nameof(pessoaAbordadaModel.IdPessoa), message: "IdPessoa inválido.");
            }

            await _pessoaRepository.RemoverPessoaAbordadaPorIdPessoa(pessoaAbordadaModel.IdPessoa);
            var pessoaAbordada = new PessoaAbordadaEntity { IdPessoa = pessoaAbordadaModel.IdPessoa, JSON = pessoaAbordadaModel.JSON };
            var idPessoaAbordada = await _pessoaRepository.InserirPessoaAbordada(pessoaAbordada);

            return idPessoaAbordada;
        }

        public async Task<PessoaAbordadaViewModel> ObterPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            var pessoaAbordada = await _pessoaRepository.ObterPessoaAbordadaPorIdPessoa(idPessoa);

            if (pessoaAbordada != null)
                return PessoaViewModelMapper.PessoaAbordadaMapper(pessoaAbordada);

            return null;
        }

        public async Task<IEnumerable<PessoaAbordadaViewModel>> ObterPessoasAbordadas()
        {
            var pessoasAbordadasModel = new List<PessoaAbordadaViewModel>();

            var pessoasAbordadas = await _pessoaRepository.ObterPessoasAbordadas();
            pessoasAbordadas.ToList()?.ForEach(pessoaAbordada =>
                pessoasAbordadasModel.Add(
                    PessoaViewModelMapper.PessoaAbordadaMapper(pessoaAbordada)
                )
            );

            return pessoasAbordadasModel;
        }

        public async Task<PessoaViewModel> PesquisarExternaPorCPF(string cpf)
        {
            string _cpf = cpf.RemoveMask();

            if (!_cpf.Has11DigitsWithoutMask())
                throw new ArgumentException("CPF deve ter 11 dígitos");

            if (!_cpf.IsValidCpf())
                throw new ArgumentException("CPF inválido");

            PessoaEntity pessoaEntity = await _pessoaRepository.PesquisarExternaPorCPF(_cpf);

            if (pessoaEntity is not null)
            {
                return PessoaViewModelMapper.PessoaMapper(pessoaEntity);
            }

            return null;
        }

        public async Task<PessoaViewModel> PesquisarPorCPF(string cpf)
        {
            string _cpf = cpf.RemoveMask();

            if (!_cpf.Has11DigitsWithoutMask())
                throw new ArgumentException("CPF deve ter 11 dígitos");

            if (!_cpf.IsValidCpf())
                throw new ArgumentException("CPF inválido");

            PessoaEntity pessoaEntity = await _pessoaRepository.PesquisarPorCPF(_cpf);

            if (pessoaEntity is not null)
            {
                return PessoaViewModelMapper.PessoaMapper(pessoaEntity);
            }

            return null;
        }

        public async Task<bool> RemoverPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            var result = await _pessoaRepository.RemoverPessoaAbordadaPorIdPessoa(idPessoa);

            return result;
        }

        #endregion Public Methods
    }
}