using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class VeiculoApplicationService : IVeiculoApplicationService
    {
        #region Private Fields

        private readonly IVeiculoRepository _veiculoRepository;

        #endregion Private Fields

        #region Public Constructors

        public VeiculoApplicationService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProprietarioViewModel> ConsultaProprietarioPorPlaca(string Placa)
        {
            var proprietario = await _veiculoRepository.ConsultaProprietarioPorPlaca(Placa);

            if (proprietario is null)
            {
                return null;
            }

            return VeiculoViewModelMapper.ProprietarioMapper(proprietario);
        }

        public async Task<VeiculoViewModel> ConsultaVeiculoPorChassi(string chassi)
        {
            var veiculo = await _veiculoRepository.ConsultaVeiculoPorChassi(chassi);

            if (veiculo is null)
            {
                return null;
            }

            return VeiculoViewModelMapper.VeiculoConsultaMapper(veiculo);
        }

        public async Task<VeiculoViewModel> ConsultaVeiculoPorPlaca(string placa)
        {
            VeiculoEntity veiculo = await _veiculoRepository.ConsultaVeiculoPorPlaca(placa);

            if (veiculo is not null)
            {
                return VeiculoViewModelMapper.VeiculoConsultaMapper(veiculo);
            }

            return null;
        }

        public async Task<VeiculoViewModel> ConsultaVeiculoPorPlacaERenavam(string placa, string renavam)
        {
            var veiculo = await _veiculoRepository.ConsultaExternaPorPlacaERenavam(placa, renavam);

            if (veiculo is not null)
            {
                return VeiculoViewModelMapper.VeiculoConsultaMapper(veiculo);
            }

            return null;
        }

        public async Task<VeiculoViewModel> ConsultaVeiculoPorRenavam(string renavam)
        {
            VeiculoEntity veiculo = await _veiculoRepository.ConsultaVeiculoPorRenavam(renavam);

            if (veiculo is not null)
            {
                return VeiculoViewModelMapper.VeiculoConsultaMapper(veiculo);
            }

            return null;
        }

        public async Task<int> InserirVeiculoAbordado(VeiculoAbordadoViewModel veiculoAbordado)
        {
            if (String.IsNullOrEmpty(veiculoAbordado.Placa) ||
                veiculoAbordado.Placa.Length < 4 ||
                veiculoAbordado.Placa.Length > 45)
            {
                throw new ArgumentException(paramName: nameof(veiculoAbordado.Placa), message: "Placa e/ou Chassi inválidos.");
            }

            veiculoAbordado.Placa = veiculoAbordado.Placa.Trim().ToUpper();

            await _veiculoRepository.RemoverVeiculoAbordadoPorPlaca(veiculoAbordado.Placa);
            var idVeiculoAbordado = await _veiculoRepository.InserirVeiculoAbordado(veiculoAbordado);

            return idVeiculoAbordado;
        }

        public async Task<IEnumerable<LocalidadeViewModel>> ObterCidadeEstadoPais()
        {
            var localidadesModel = new List<LocalidadeViewModel>();

            var cidades = await _veiculoRepository.ObterCidades();
            cidades.ToList()?.ForEach(cidade =>
            {
                var estado = EstadoViewModel.ListaUnidadesFederativasBrasil().FirstOrDefault(uf => uf.Sigla == cidade.UF?.ToUpper())?.Nome;

                if (!String.IsNullOrEmpty(estado))
                    localidadesModel.Add(VeiculoViewModelMapper.CidadeEstadoPaisMapper(cidade, estado, "Brasil"));
            });

            return localidadesModel;
        }

        public async Task<IEnumerable<CidadeViewModel>> ObterCidades()
        {
            var cidadesModel = new List<CidadeViewModel>();

            var cidades = await _veiculoRepository.ObterCidades();
            cidades.ToList()?.ForEach(cidade =>
                cidadesModel.Add(
                    VeiculoViewModelMapper.CidadeMapper(cidade)
                )
            );

            return cidadesModel;
        }

        public async Task<IEnumerable<CorViewModel>> ObterCores()
        {
            var coresModel = new List<CorViewModel>();

            var cores = await _veiculoRepository.ObterCores();
            cores.ToList()?.ForEach(cor =>
                coresModel.Add(
                    VeiculoViewModelMapper.CorMapper(cor)
                )
            );

            return coresModel;
        }

        public async Task<CorModeloEspecieViewModel> ObterCoresModelosEspecies()
        {
            var cores = await _veiculoRepository.ObterCores();
            var modelos = await _veiculoRepository.ObterMarcas();
            var especies = await _veiculoRepository.ObterEspecies();

            var coresModelosEspeciesModel = new CorModeloEspecieViewModel(
                cores?.Select(c => c.Descricao).ToList(),
                modelos?.Select(m => m.Descricao).ToList(),
                especies?.Select(e => e.Descricao).ToList()
            );

            return coresModelosEspeciesModel;
        }

        public async Task<IEnumerable<EspecieViewModel>> ObterEspecies()
        {
            var especiesModel = new List<EspecieViewModel>();

            var especies = await _veiculoRepository.ObterEspecies();
            especies.ToList()?.ForEach(especie =>
                especiesModel.Add(
                    VeiculoViewModelMapper.EspecieMapper(especie)
                )
            );

            return especiesModel;
        }

        public IEnumerable<EstadoViewModel> ObterEstados()
        {
            return EstadoViewModel.ListaUnidadesFederativasBrasil();
        }

        public async Task<PaginacaoViewModel<MarcaModeloViewModel>> ObterMarcasModelos(int? page = 0, int? limit = 0)
        {
            var marcasModel = new List<MarcaModeloViewModel>();

            var marcas = await _veiculoRepository.ObterMarcas(page, limit);
            marcas.ToList()?.ForEach(marca =>
                marcasModel.Add(
                    VeiculoViewModelMapper.MarcaModeloMapper(marca)
                )
            );

            if (marcasModel == null || !marcasModel.Any())
                return null;

            var totalDeRegistros = await _veiculoRepository.TotalDeRegistros_Marcas();
            var paginationResult = new PaginacaoViewModel<MarcaModeloViewModel>(marcasModel, totalDeRegistros, page, limit);

            return paginationResult;
        }

        public async Task<PaginacaoViewModel<MarcaModeloTipoViewModel>> ObterMarcasModelosTiposEspecies(int? page = 0, int? limit = 0)
        {
            var marcasModeloTipoModel = new List<MarcaModeloTipoViewModel>();

            var marcas = await _veiculoRepository.ObterMarcasModelosTiposEspecies(page, limit);
            marcasModeloTipoModel = marcas.ToList()?.ConvertAll(
                new Converter<MarcaModeloTipoEntity, MarcaModeloTipoViewModel>(VeiculoViewModelMapper.MarcasModeloTipoMapper)
            );

            if (marcasModeloTipoModel == null || !marcasModeloTipoModel.Any())
                return null;

            var totalDeRegistros = await _veiculoRepository.TotalDeRegistros_MarcasModelosTiposEspecies();
            var paginationResult = new PaginacaoViewModel<MarcaModeloTipoViewModel>(marcasModeloTipoModel, totalDeRegistros, page, limit);

            return paginationResult;
        }

        public async Task<IEnumerable<PaisViewModel>> ObterPaises()
        {
            var paisesModel = new List<PaisViewModel>();

            var paises = await _veiculoRepository.ObterPaises();
            paises.ToList()?.ForEach(pais =>
                paisesModel.Add(
                    VeiculoViewModelMapper.PaisMapper(pais)
                )
            );

            return paisesModel;
        }

        public async Task<VeiculoAbordadoViewModel> ObterVeiculoAbordadoPorPlaca(string placa)
        {
            var veiculoAbordado = await _veiculoRepository.ObterVeiculoAbordadoPorPlaca(placa);

            if (veiculoAbordado != null)
                return VeiculoViewModelMapper.VeiculoAbordadoMapper(veiculoAbordado);

            return null;
        }

        public async Task<IEnumerable<VeiculoAbordadoViewModel>> ObterVeiculosAbordados()
        {
            var veiculoAbordadoModel = new List<VeiculoAbordadoViewModel>();

            var veiculosAbordados = await _veiculoRepository.ObterVeiculosAbordados();
            veiculosAbordados.ToList()?.ForEach(veiculoAbordado =>
                veiculoAbordadoModel.Add(
                    VeiculoViewModelMapper.VeiculoAbordadoMapper(veiculoAbordado)
                )
            );

            return veiculoAbordadoModel;
        }

        public async Task<bool> RemoverVeiculoAbordadoPorPlaca(string placa)
        {
            var result = await _veiculoRepository.RemoverVeiculoAbordadoPorPlaca(placa);
            return result;
        }

        #endregion Public Methods
    }
}