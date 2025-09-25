namespace Talonario.Api.Server.Application.ViewModels
{
    public class UsuariosViewModel
    {
        #region Public Constructors

        public UsuariosViewModel()
        {
        }

        public UsuariosViewModel(
            long id,
            string usuario,
            string cpf,
            string email,
            string permissoes,
            string empresa,
            int idEmpresa,
            string competencia,
            string senha,
            bool? ativo
        )
        {
            Id = id;
            Usuario = usuario;
            CPF = cpf;
            Email = email;
            Permissoes = permissoes;
            Empresa = empresa;
            IdEmpresa = idEmpresa.ToString();
            Competencia = competencia;
            Senha = senha;
            Ativo = ativo;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool? Ativo { get; set; }

        public string Competencia { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        public long Id { get; set; }

        public string IdEmpresa { get; set; }

        public string Permissoes { get; set; }

        public string Senha { get; set; }

        public string Usuario { get; set; }

        #endregion Public Properties
    }
}