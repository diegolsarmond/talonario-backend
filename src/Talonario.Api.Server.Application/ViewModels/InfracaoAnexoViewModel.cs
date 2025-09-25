namespace Talonario.Api.Server.Application.ViewModels
{
    public class InfracaoAnexoViewModel
    {
        #region Public Constructors

        public InfracaoAnexoViewModel(
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