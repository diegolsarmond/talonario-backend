using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ITamaRepository
    {
        Task<TamaEntity> CadastrarTermoAdocaoMedidaAdministrativaAsync(TamaEntity entity);
    }
}