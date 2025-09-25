using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class CorViewModel
    {
        #region Public Constructors

        public CorViewModel(
            int idCor,
            string descricao,
            DateTime dataInclusao
        )
        {
            IdCor = idCor;
            Descricao = descricao;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Descricao { get; set; }

        public int IdCor { get; set; }

        #endregion Public Properties
    }
}