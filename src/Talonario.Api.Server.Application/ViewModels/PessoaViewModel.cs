using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class PessoaViewModel
    {
        #region Public Properties

        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Habilitacao { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string Renach { get; set; }
        public string Sexo { get; set; }
        public string UFHabilitacao { get; set; }

        #endregion Public Properties
    }
}