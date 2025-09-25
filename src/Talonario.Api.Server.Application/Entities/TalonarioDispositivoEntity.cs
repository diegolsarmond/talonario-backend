using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Inf_TalonarioDispositivo")]
    public class TalonarioDispositivoEntity
    {
        #region Public Constructors

        public TalonarioDispositivoEntity()
        {
        }

        public TalonarioDispositivoEntity(
            int id,
            string idDispositivo,
            string sequencia
        )
        {
            Id = id;
            IdDispositivo = idDispositivo;
            Sequencia = sequencia;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; set; }

        public string IdDispositivo { get; set; }

        public string Sequencia { get; set; }

        #endregion Public Properties
    }
}