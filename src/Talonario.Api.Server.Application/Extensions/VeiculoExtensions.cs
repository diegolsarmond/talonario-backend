using System.Text;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Enums;

namespace Talonario.Api.Server.Application.Extensions
{
    public static class VeiculoExtensions
    {
        #region Public Methods

        /// <summary>
        /// Define o tipo da mensagem do veículo
        /// </summary>
        public static TipoMensagem DefineTipoDeMensagem(this VeiculoEntity veiculoEntity)
        {
            if (veiculoEntity.FurtadoOuRoubado)
            {
                return TipoMensagem.FurtoOuRoubo;
            }

            if (veiculoEntity.TotalAutuacoes > 0 || veiculoEntity.TotalMultas > 0)
            {
                return TipoMensagem.AutuacaoOuMulta;
            }

            return TipoMensagem.Ok;
        }

        /// <summary>
        /// Gera mensagem sobre a situação do veículo
        /// </summary>
        public static string GeraMensagem(this VeiculoEntity veiculoEntity)
        {
            if (veiculoEntity.FurtadoOuRoubado
                && veiculoEntity.TotalAutuacoes == 0
                && veiculoEntity.TotalMultas == 0)
            {
                return "Veículo com registro de furto e/ou roubo";
            }

            if (veiculoEntity.FurtadoOuRoubado
                && (veiculoEntity.TotalAutuacoes != 0
                || veiculoEntity.TotalMultas != 0))
            {
                StringBuilder sb = new();
                sb.Append("Veículo com registro de furto e/ou roubo, ");
                sb.Append($"com {veiculoEntity.TotalAutuacoes} autuação(ões) ");
                sb.Append($"e {veiculoEntity.TotalMultas} multa(s)");

                return sb.ToString();
            }

            if (veiculoEntity.TotalAutuacoes != 0
                || veiculoEntity.TotalMultas != 0)
            {
                return "Veículo com débitos tributários ou impedimentos de circulação";
            }

            return "Este veículo não tem autuação e não tem multas";
        }

        #endregion Public Methods
    }
}