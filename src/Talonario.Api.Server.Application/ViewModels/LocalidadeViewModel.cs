namespace Talonario.Api.Server.Application.ViewModels
{
    public class LocalidadeViewModel
    {
        #region Public Constructors

        public LocalidadeViewModel(
            string codLocal,
            string cidade,
            string estado,
            string uf,
            string pais
        )
        {
            CodLocal = codLocal;
            Cidade = cidade;
            Estado = estado;
            UF = uf;
            Pais = pais;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Cidade { get; set; }

        public string CodLocal { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }

        public string UF { get; set; }

        #endregion Public Properties
    }
}