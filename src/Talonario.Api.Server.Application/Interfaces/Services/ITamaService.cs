using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ITamaService
    {
        Task<TamaViewModel> CadastrarTamaAsync(TamaInputCompletoViewModel input);

        //Task<TamaViewModel> CadastrarTamaCompletoAsync(TamaViewModel tama, TamaInputCompletoViewModel complemento);
    }
}