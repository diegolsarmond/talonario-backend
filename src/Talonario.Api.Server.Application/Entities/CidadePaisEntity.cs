using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Gen_CidadePais")]
    public class CidadePaisEntity
    {
        #region Public Constructors

        public CidadePaisEntity()
        {
        }

        public CidadePaisEntity(
            long idCidadePais,
            string codLocal,
            string tipoLocal,
            string nomeLocal,
            string uf
        )
        {
            this.idCidadePais = idCidadePais;
            CodLocal = codLocal;
            TipoLocal = tipoLocal;
            NomeLocal = nomeLocal;
            UF = uf;
        }

        #endregion Public Constructors

        #region Public Properties

        public string CodLocal { get; set; }

        public long idCidadePais { get; set; }

        public string NomeLocal { get; set; }

        public string TipoLocal { get; set; }

        public string UF { get; set; }

        #endregion Public Properties
    }
}