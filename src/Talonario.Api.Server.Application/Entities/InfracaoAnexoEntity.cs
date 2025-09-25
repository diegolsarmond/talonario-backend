using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Inf_InfracaoAnexo")]
    public class InfracaoAnexoEntity
    {
        #region Public Constructors

        public InfracaoAnexoEntity()
        {
        }

        public InfracaoAnexoEntity(
            int id,
            string ait,
            string anexoBase64)
        {
            Id = id;
            AIT = ait;
            AnexoBase64 = anexoBase64;
        }

        #endregion Public Constructors

        #region Public Properties

        public string AIT { get; set; }

        public string AnexoBase64 { get; set; }

        public int Id { get; set; }

        #endregion Public Properties
    }
}