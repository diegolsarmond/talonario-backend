using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IGoogleMapsApplicationService
    {
        #region Public Methods

        Task<EnderecoViewModel> ObterEnderecoPorCep(string cep);

        Task<EnderecoViewModel> ObterEnderecoPorCoordenadas(string latitude, string longitude);

        #endregion Public Methods
    }
}