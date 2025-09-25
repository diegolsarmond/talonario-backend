using System.Collections.Generic;
using System.Threading.Tasks;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ITermoAdocaoService
    {
        Task<List<object>> ObterItensGeraisAsync();
    }
}