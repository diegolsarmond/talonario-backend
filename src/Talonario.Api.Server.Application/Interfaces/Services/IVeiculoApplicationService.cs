using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IVeiculoApplicationService
    {
        #region Public Methods

        Task<ProprietarioViewModel> ConsultaProprietarioPorPlaca(string Placa);

        Task<VeiculoViewModel> ConsultaVeiculoPorChassi(string chassi);

        Task<VeiculoViewModel> ConsultaVeiculoPorPlaca(string placa);

        Task<VeiculoViewModel> ConsultaVeiculoPorPlacaERenavam(string placa, string renavam);

        Task<VeiculoViewModel> ConsultaVeiculoPorRenavam(string renavam);

        Task<int> InserirVeiculoAbordado(VeiculoAbordadoViewModel veiculoAbordado);

        Task<IEnumerable<LocalidadeViewModel>> ObterCidadeEstadoPais();

        Task<IEnumerable<CidadeViewModel>> ObterCidades();

        Task<IEnumerable<CorViewModel>> ObterCores();

        Task<CorModeloEspecieViewModel> ObterCoresModelosEspecies();

        Task<IEnumerable<EspecieViewModel>> ObterEspecies();

        IEnumerable<EstadoViewModel> ObterEstados();

        Task<PaginacaoViewModel<MarcaModeloViewModel>> ObterMarcasModelos(int? page = 0, int? limit = 0);

        Task<PaginacaoViewModel<MarcaModeloTipoViewModel>> ObterMarcasModelosTiposEspecies(int? page = 0, int? limit = 0);

        Task<IEnumerable<PaisViewModel>> ObterPaises();

        Task<VeiculoAbordadoViewModel> ObterVeiculoAbordadoPorPlaca(string placa);

        Task<IEnumerable<VeiculoAbordadoViewModel>> ObterVeiculosAbordados();

        Task<bool> RemoverVeiculoAbordadoPorPlaca(string placa);

        #endregion Public Methods
    }
}