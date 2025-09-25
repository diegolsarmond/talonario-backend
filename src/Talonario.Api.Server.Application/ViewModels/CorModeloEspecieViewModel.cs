using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class CorModeloEspecieViewModel
    {
        #region Public Constructors

        public CorModeloEspecieViewModel(
            List<string> cores,
            List<string> modelos,
            List<string> especies
        )
        {
            Cores = cores;
            Modelos = modelos;
            Especies = especies;
        }

        #endregion Public Constructors

        #region Public Properties

        public List<string> Cores { get; set; }

        public List<string> Especies { get; set; }

        public List<string> Modelos { get; set; }

        #endregion Public Properties
    }
}