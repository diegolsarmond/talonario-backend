using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class EspecieViewModel
    {
        #region Public Constructors

        public EspecieViewModel(
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

        #endregion Public Properties
    }
}