using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Application.Services
{
    public class TermoAdocaoService : ITermoAdocaoService
    {
        private readonly ITermoAdocaoRepository _repository;

        public TermoAdocaoService(ITermoAdocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<object>> ObterItensGeraisAsync()
        {
            var estadoValores = await _repository.ObterValoresDistintosEstadoLatariaAsync();
            var transporteValores = await _repository.ObterValoresDistintosTransporteAsync();
            var documentosPossiveis = await _repository.ObterDocumentosPossiveisAsync();
            var equipamentosObrigatorios = await _repository.ObterEquipamentosObrigatoriosAsync();
            // var documentosRecolhidos = await _repository.ObterDocumentosRecolhidosAsync();

            var listaEstado = estadoValores.Select((valor, index) => new
            {
                id = index,
                nome = valor,
                tipo = "EstadoGeralLatariaPintura"
            }).ToList();

            var listaTransporte = transporteValores.Select((valor, index) => new
            {
                id = index,
                nome = valor,
                tipo = "TransporteLocalRecolhimento"
            }).ToList();

            var listaDocPossiveis = documentosPossiveis.Select(doc => new
            {
                id = doc.IdDocumentoRecolhido,
                nome = doc.Titulo,
                tipo = "DocumentosPossiveis"
            }).ToList();
            var listaEquipamentos = equipamentosObrigatorios.Select(e => new
            {
                id = e.IdEquipamentoObrigatorio,
                nome = e.Titulo,
                tipo = "EquipamentosObrigatoriosAusentes"
            }).ToList();

            // var listaDocRecolhidos = documentosRecolhidos.Select(doc => new { id =
            // doc.IdDocumentoRecolhido, nome = doc.Titulo, tipo = "DocumentosRecolhidos" }).ToList();

            var listaFinal = new List<object>();
            listaFinal.AddRange(listaEstado);
            listaFinal.AddRange(listaTransporte);
            listaFinal.AddRange(listaDocPossiveis);
            listaFinal.AddRange(listaEquipamentos);

            return listaFinal;
        }
    }
}