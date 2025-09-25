using System;

namespace Talonario.Api.Server.Application.Entities
{
    public class UsuarioLogadoEntity
    {
        #region Public Properties

        public string CPF { get; set; }
        public DateTime DataAutenticacao { get; set; }
        public int Id { get; set; }
        public string IdDispositivo { get; set; }

        #endregion Public Properties
    }
}