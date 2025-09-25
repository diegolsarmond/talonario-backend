using System;

namespace Talonario.Api.Server.Application.Entities
{
    public class SolicitacaoCancelamentoAITEntity
    {
        public int Id { get; set; }
        public int IdAutoInfracao { get; set; }
        public string NumeroAutoInfracao { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string MotivoCancelamento { get; set; }
        public string CPFAgente { get; set; }
        public int SituacaoCancelamento { get; set; } = 0;
        public string ObservacaoCancelamento { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string UsuarioCancelamento { get; set; }
        public DateTime DataHoraSolicitacao { get; set; } = DateTime.UtcNow;
    }
}