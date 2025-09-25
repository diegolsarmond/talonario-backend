namespace Talonario.Api.Server.Application.ViewModels
{
    public class PessoaAbordadaViewModel
    {
        #region Public Constructors

        public PessoaAbordadaViewModel(
            int id,
            string idPessoa,
            string json
        )
        {
            Id = id;
            IdPessoa = idPessoa;
            JSON = json;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; set; }

        public string IdPessoa { get; set; }

        public string JSON { get; set; }

        #endregion Public Properties
    }
}