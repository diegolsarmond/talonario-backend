using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class CondutorViewModel
    {
        #region Public Properties

        public string CategoriaCNH { set; get; }
        public string CPF { set; get; }
        public DateTime DataNascimento { set; get; }
        public DateTime DataValidadeCNH { set; get; }
        public string Nome { set; get; }
        public string NomeMae { set; get; }
        public string NomePai { set; get; }
        public string NumeroRegistro { set; get; }
        public string Sexo { set; get; }
        public string UFHabilitacao { get; set; }

        #endregion Public Properties
    }
}