using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ICancelamentoAITService
    {
        Task<int> RegistrarSolicitacaoAsync(SolicitacaoCancelamentoAITViewModel viewModel);
    }
}