using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Rev_Especie")]
    public class EspecieEntity
    {
        #region Public Constructors

        public EspecieEntity()
        {
        }

        public EspecieEntity(
            int idEspecie,
            string descricao,
            string descricaoAbreviada,
            DateTime dataInclusao
        )
        {
            IdEspecie = idEspecie;
            Descricao = descricao;
            DescricaoAbreviada = descricaoAbreviada;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Descricao { get; set; }

        public string DescricaoAbreviada { get; set; }

        public int IdEspecie { get; set; }

        public int IdSessao { get; set; }

        public string Maint { get; set; }

        public string UsuarioInclusao { get; set; }

        #endregion Public Properties
    }
}