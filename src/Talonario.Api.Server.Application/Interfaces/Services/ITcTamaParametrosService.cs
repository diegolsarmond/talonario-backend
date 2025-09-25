using System.Threading.Tasks;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ITcTamaParametrosService
    {
        Task<object> ObterTodosParametros();
    }
}