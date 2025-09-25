using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class MarcaModeloViewModel
    {
        #region Public Constructors

        public MarcaModeloViewModel(
            int idMarca,
            string descricao,
            int? idEspecie,
            DateTime dataInclusao
        )
        {
            IdMarca = idMarca;
            Descricao = descricao;
            IdEspecie = idEspecie;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Descricao { get; set; }

        public int? IdEspecie { get; set; }

        public int IdMarca { get; set; }

        #endregion Public Properties
    }
}