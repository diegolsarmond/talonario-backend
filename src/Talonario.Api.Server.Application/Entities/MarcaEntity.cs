using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("Rev_Marca")]
    public class MarcaEntity
    {
        #region Public Constructors

        public MarcaEntity()
        {
        }

        public MarcaEntity(
            int idMarca,
            string descricao,
            int? idEspecie,
            DateTime dataInclusao
        )
        {
            IdMarca = idMarca;
            Descricao = descricao;
            this.idEspecie = idEspecie;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Descricao { get; set; }

        public int? idEspecie { get; set; }

        public int IdMarca { get; set; }

        public int IdSessao { get; set; }

        public int? idTipoVeiculo { get; set; }

        public DateTime? IPVAData { get; set; }

        public int? IPVAFaixa { get; set; }

        public string IPVAOperador { get; set; }

        public string UsuarioInclusao { get; set; }

        #endregion Public Properties
    }
}