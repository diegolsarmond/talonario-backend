namespace Talonario.Api.Server.Application.ViewModels
{
    public class CidadeViewModel
    {
        #region Public Constructors

        public CidadeViewModel(
            long idCidadePais,
            string codLocal,
            string tipoLocal,
            string nomeLocal,
            string uf
        )
        {
            IdCidadePais = idCidadePais;
            CodLocal = codLocal;
            TipoLocal = tipoLocal;
            NomeLocal = nomeLocal;
            UF = uf;
        }

        #endregion Public Constructors

        #region Public Properties

        public string CodLocal { get; set; }

        public long IdCidadePais { get; set; }

        public string NomeLocal { get; set; }

        public string TipoLocal { get; set; }

        public string UF { get; set; }

        #endregion Public Properties
    }
}