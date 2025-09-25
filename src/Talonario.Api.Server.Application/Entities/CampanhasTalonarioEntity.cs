using System;

namespace Talonario.Api.Server.Application.Entities
{
    public class InfCampanhasTalonario
    {
        #region Public Properties

        public bool Ativo { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
        public DateTime? DataHoraInativacao { get; set; }
        public DateTime? DataHoraInclusao { get; set; }
        public DateTime? DataInicio { get; set; }
        public string Descricao { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string UsuarioInativacao { get; set; }
        public string UsuarioInclusao { get; set; }

        #endregion Public Properties
    }
}