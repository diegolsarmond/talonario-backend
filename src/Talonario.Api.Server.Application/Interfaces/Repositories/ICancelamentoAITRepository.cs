using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ICancelamentoAITRepository
    {
        Task<int> InserirAsync(SolicitacaoCancelamentoAITEntity entity);

        Task<int?> ObterIdAutoInfracaoAsync(string numeroAutoInfracao);
    }
}