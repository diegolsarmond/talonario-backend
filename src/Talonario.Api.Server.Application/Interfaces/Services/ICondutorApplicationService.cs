using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ICondutorApplicationService
    {
        #region Public Methods

        Task<CondutorViewModel> PesquisarExternaPorCpf(string cpf);

        Task<CondutorViewModel> PesquisarPorCpf(string cpf);

        #endregion Public Methods
    }
}