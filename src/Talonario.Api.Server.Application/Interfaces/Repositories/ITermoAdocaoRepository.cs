using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ITermoAdocaoRepository
    {
        Task<IEnumerable<string>> ObterValoresDistintosEstadoLatariaAsync();

        Task<IEnumerable<string>> ObterValoresDistintosTransporteAsync();

        Task<IEnumerable<DocumentoViewModel>> ObterDocumentosPossiveisAsync();

        Task<IEnumerable<EquipamentoObrigatorioEntity>> ObterEquipamentosObrigatoriosAsync();

        //Task<IEnumerable<DocumentoViewModel>> ObterDocumentosRecolhidosAsync();
    }
}