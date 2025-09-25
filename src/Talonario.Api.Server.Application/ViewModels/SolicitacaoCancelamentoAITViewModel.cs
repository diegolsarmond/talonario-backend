using System.ComponentModel.DataAnnotations;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class SolicitacaoCancelamentoAITViewModel
    {
        [Required(ErrorMessage = "Número do Auto é obrigatório")]
        public string NumeroAutoInfracao { get; set; }

        [Required]
        public string Placa { get; set; }

        public string Chassi { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string MotivoCancelamento { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido")]
        public string CPFAgente { get; set; }

        public int SituacaoCancelamento { get; set; } = 0;
    }
}