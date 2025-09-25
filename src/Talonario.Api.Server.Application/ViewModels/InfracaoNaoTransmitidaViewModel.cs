using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class InfracaoNaoTransmitidaViewModel
    {
        #region Public Constructors

        public InfracaoNaoTransmitidaViewModel(
            int id,
            string ait,
            string json,
            string tipo,
            DateTime? dataCancelamento,
            DateTime? dataEnviado,
            string motivoProcessamento = null,
            DateTime? dataInclusao = null
        )
        {
            Id = id;
            AIT = ait;
            JSON = json;
            Tipo = tipo;
            DataCancelamento = dataCancelamento;
            DataEnviado = dataEnviado;
            MotivoProcessamento = motivoProcessamento;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public string AIT { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public DateTime? DataEnviado { get; set; }

        public int Id { get; set; }

        public string JSON { get; set; }

        public string Tipo { get; set; }

        public string MotivoProcessamento { get; set; }

        public DateTime? DataInclusao { get; set; }

        #endregion Public Properties
    }
}