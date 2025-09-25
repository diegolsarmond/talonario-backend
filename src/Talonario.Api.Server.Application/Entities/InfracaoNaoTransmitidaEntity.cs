using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Inf_InfracaoNaoTransmitida")]
    public class InfracaoNaoTransmitidaEntity
    {
        #region Public Constructors

        public InfracaoNaoTransmitidaEntity()
        {
        }

        public InfracaoNaoTransmitidaEntity(
            int id,
            string ait,
            string json,
            string tipo,
            DateTime? dataCancelamento,
            DateTime? dataEnviado
        )
        {
            Id = id;
            AIT = ait;
            JSON = json;
            Tipo = tipo;
            DataCancelamento = dataCancelamento;
            DataEnviado = dataEnviado;
        }

        #endregion Public Constructors

        #region Public Properties

        public string AIT { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public DateTime? DataEnviado { get; set; }

        public int Id { get; set; }

        public string JSON { get; set; }

        public string Tipo { get; set; }

        #endregion Public Properties
    }
}