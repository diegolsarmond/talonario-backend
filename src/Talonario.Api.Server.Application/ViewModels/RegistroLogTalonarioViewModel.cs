using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class RegistroLogTalonarioViewModel
    {
        public string NomeAgente { get; set; }
        public string CpfAgente { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public string Dispositivo { get; set; }
        public string Modulo { get; set; }
        public string AppVersao { get; set; }
        public bool? Falha { get; set; } = false;
    }
}