using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Rev_Cor")]
    public class CorEntity
    {
        #region Public Constructors

        public CorEntity()
        {
        }

        public CorEntity(
            int idCor,
            string descricao,
            DateTime dataInclusao
        )
        {
            this.idCor = idCor;
            Descricao = descricao;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Descricao { get; set; }

        public int idCor { get; set; }

        public int IdSessao { get; set; }

        public string UsuarioInclusao { get; set; }

        #endregion Public Properties
    }
}