namespace Talonario.Api.Server.Application.ViewModels
{
    public class UsuarioLoginInputModel
    {
        #region Public Constructors

        public UsuarioLoginInputModel(
            string cpf,
            string senha,
            string idDispositivo
        )
        {
            CPF = cpf;
            Senha = senha;
            IdDispositivo = idDispositivo;
        }

        #endregion Public Constructors

        #region Public Properties

        public string CPF { get; set; }

        public string IdDispositivo { get; set; }

        public string Senha { get; set; }

        #endregion Public Properties
    }
}