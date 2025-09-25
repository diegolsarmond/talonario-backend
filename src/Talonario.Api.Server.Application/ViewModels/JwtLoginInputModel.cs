namespace Talonario.Api.Server.Application.ViewModels
{
    public class JwtLoginInputModel
    {
        #region Public Constructors

        public JwtLoginInputModel()
        {
        }

        public JwtLoginInputModel(
            string usuario,
            string senha
        )
        {
            Usuario = usuario;
            Senha = senha;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Senha { get; set; }

        public string Usuario { get; set; }

        #endregion Public Properties
    }
}