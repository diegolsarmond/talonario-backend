namespace Talonario.Api.Server.Application.ViewModels
{
    public class EnderecoViewModel
    {
        #region Public Constructors

        public EnderecoViewModel()
        {
        }

        public EnderecoViewModel(
            string cep,
            string logradouro,
            string numero,
            string bairro,
            string cidade,
            string uf,
            string pais
        )
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Pais = pais;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Pais { get; set; }

        public string UF { get; set; }

        #endregion Public Properties
    }
}