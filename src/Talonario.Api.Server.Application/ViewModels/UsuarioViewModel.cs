namespace Talonario.Api.Server.Application.ViewModels
{
    public class UsuarioViewModel
    {
        #region Public Constructors

        public UsuarioViewModel()
        {
        }

        public UsuarioViewModel(
            long id,
            string usuario,
            string cpf,
            string email,
            string permissoes,
            string empresa,
            int idEmpresa,
            string competencia,
            string token,
            string senha,
            int idTalonarioDispositivo,
            string idDispositivo,
            string sequencia,
            bool? ativo,
            int matricula,
            int matriculaAgente
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
            Token = (!string.IsNullOrEmpty(token)) ? $"Bearer {token}" : string.Empty;
            Senha = senha;
            IdTalonarioDispositivo = CompletaRetornoString(idTalonarioDispositivo.ToString(), '0', 4);
            IdDispositivo = idDispositivo;
            Sequencia = (sequencia != null) ? CompletaRetornoString(sequencia, '0', 4) : null;
            Ativo = ativo;
            Matricula = matricula;
            MatriculaAgente = matriculaAgente;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool? Ativo { get; set; }

        public string Competencia { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        public long Id { get; set; }

        public string IdDispositivo { get; set; }

        public string IdEmpresa { get; set; }

        public string IdTalonarioDispositivo { get; set; }

        public int Matricula { get; set; }

        public int MatriculaAgente { get; set; }

        public string Permissoes { get; set; }

        public string Senha { get; set; }

        public string Sequencia { get; set; }

        public string Token { get; set; }

        public string Usuario { get; set; }

        #endregion Public Properties

        #region Private Methods

        private string CompletaRetornoString(string texto, char complemento, int tamanho)
        {
            string result = texto;
            if (texto.Length < tamanho)
                result = texto.PadLeft(tamanho, complemento);

            return result;
        }

        #endregion Private Methods
    }
}