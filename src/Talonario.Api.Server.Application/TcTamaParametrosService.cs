using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Application.Services
{
    public class TcTamaParametrosService : ITcTamaParametrosService
    {
        private readonly ITcTamaParametrosRepository _tcTamaParametrosRepository;

        public TcTamaParametrosService(ITcTamaParametrosRepository tcTamaParametrosRepository)
        {
            _tcTamaParametrosRepository = tcTamaParametrosRepository;
        }

        public async Task<object> ObterTodosParametros()
        {
            var memoria = await _tcTamaParametrosRepository.ObterMemoria();
            var orientacao = await _tcTamaParametrosRepository.ObterOrientacao();
            var capacidadeMotora = await _tcTamaParametrosRepository.ObterCapacidadeMotora();
            var atitude = await _tcTamaParametrosRepository.ObterAtitude();
            var aparencia = await _tcTamaParametrosRepository.ObterAparencia();
            var descricaoCondutor = await _tcTamaParametrosRepository.ObterDescricaoCondutor();

            var resultado = memoria.Select(x => new
            {
                id = x.IdAvaliacaoCondutorMemoria,
                nome = x.Titulo,
                tipo = "memoria"
            })
            .Concat(orientacao.Select(x => new
            {
                id = x.IdAvaliacaoCondutorOrientacao,
                nome = x.Titulo,
                tipo = "orientacao"
            }))
            .Concat(capacidadeMotora.Select(x => new
            {
                id = x.IdAvaliacaoCondutorCapacidadeMotora,
                nome = x.Titulo,
                tipo = "capacidade"
            }))
            .Concat(atitude.Select(x => new
            {
                id = x.IdAvaliacaoCondutorAtitude,
                nome = x.Titulo,
                tipo = "atitude"
            }))
            .Concat(aparencia.Select(x => new
            {
                id = x.IdAvaliacaoCondutorAparencia,
                nome = x.Titulo,
                tipo = "aparencia"
            }))
            .Concat(descricaoCondutor.Select(x => new
            {
                id = x.IdDescricaoCondutor,
                nome = x.Titulo,
                tipo = "descricao"
            }))
            .ToList();

            return resultado;
        }
    }
}