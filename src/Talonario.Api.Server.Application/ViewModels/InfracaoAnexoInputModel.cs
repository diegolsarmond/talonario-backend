using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class InfracaoAnexoInputModel
    {
        #region Public Properties

        public string AIT { get; set; }

        public List<string> AnexoBase64 { get; set; }

        #endregion Public Properties
    }
}