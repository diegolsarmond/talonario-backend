namespace Talonario.Api.Server.Application.ViewModels
{
    public class PaisViewModel
    {
        #region Public Constructors

        public PaisViewModel(
            long idCidadePais,
            string tipoLocal,
            string nomeLocal
        )
        {
            IdCidadePais = idCidadePais;
            TipoLocal = tipoLocal;
            NomeLocal = nomeLocal;
        }

        #endregion Public Constructors

        #region Public Properties

        public long IdCidadePais { get; set; }

        public string NomeLocal { get; set; }

        public string TipoLocal { get; set; }

        #endregion Public Properties
    }
}