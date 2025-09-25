using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities.TcTama;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ITcTamaParametrosRepository
    {
        Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorMemoria>> ObterMemoria();

        Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorOrientacao>> ObterOrientacao();

        Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorCapacidadeMotora>> ObterCapacidadeMotora();

        Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorAtitude>> ObterAtitude();

        Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorAparencia>> ObterAparencia();

        Task<IEnumerable<TcTamaParametrosEntity.DescricaoCondutor>> ObterDescricaoCondutor();
    }
}