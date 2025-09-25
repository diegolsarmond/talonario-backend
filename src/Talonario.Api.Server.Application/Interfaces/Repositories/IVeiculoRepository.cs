using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IVeiculoRepository
    {
        #region Public Methods

        Task<VeiculoEntity> ConsultaExternaPorPlacaERenavam(string Placa, string Renavam);

        Task<ProprietarioEntity> ConsultaProprietarioPorPlaca(string Placa);

        Task<VeiculoEntity> ConsultaVeiculoPorChassi(string chassi);

        Task<VeiculoEntity> ConsultaVeiculoPorPlaca(string placa);

        Task<VeiculoEntity> ConsultaVeiculoPorRenavam(string renavam);

        Task<int> InserirVeiculoAbordado(VeiculoAbordadoViewModel veiculoAbordado);

        Task<IEnumerable<CidadePaisEntity>> ObterCidades();

        Task<IEnumerable<CorEntity>> ObterCores();

        Task<IEnumerable<EspecieEntity>> ObterEspecies();

        Task<IEnumerable<MarcaEntity>> ObterMarcas(int? page = 0, int? limit = 0);

        Task<IEnumerable<MarcaModeloTipoEntity>> ObterMarcasModelosTiposEspecies(int? page = 0, int? limit = 0);

        Task<IEnumerable<CidadePaisEntity>> ObterPaises();

        Task<VeiculoAbordadoEntity> ObterVeiculoAbordadoPorPlaca(string placa);

        Task<IEnumerable<VeiculoAbordadoEntity>> ObterVeiculosAbordados();

        Task<bool> RemoverVeiculoAbordadoPorPlaca(string placa);

        Task<int> TotalDeRegistros_Marcas();

        Task<int> TotalDeRegistros_MarcasModelosTiposEspecies();

        #endregion Public Methods
    }
}