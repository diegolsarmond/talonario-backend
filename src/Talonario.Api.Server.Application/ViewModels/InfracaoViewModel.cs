using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class InfracaoViewModel
    {
        #region Public Properties

        public string Abordagem { get; set; }
        public string Chassi { get; set; }
        public string CodigoInfracao { get; set; }
        public string CodigoMunicipio { get; set; }
        public int CodigoOrgaoAutuador { get; set; }
        public string CodigoTipoInstrumento { get; set; }
        public string CodLocalVeiculo { get; set; }
        public string CpfAgente { get; set; }
        public string CPFCondutor { get; set; }
        public DateTime DataInfracao { get; set; }
        public string Desdobramento { get; set; }
        public int? IndicadorAssinatura { get; set; }
        public string InstrumentoAfericao { get; set; }
        public string Local { get; set; }
        public string MatriculaAgente { get; set; }
        public string MedicaoConsiderada { get; set; }
        public string MedicaoReal { get; set; }
        public string NomeAgente { get; set; }
        public string NomeCondutor { get; set; }
        public string NumeroAuto { get; set; }
        public string NumeroCNH { get; set; }
        public string Observacao { get; set; }
        public string Placa { get; set; }
        public string RecolheACC { get; set; }
        public string RecolheCLRV { get; set; }
        public string RecolheCNH { get; set; }
        public string RecolheCRV { get; set; }
        public string RecolhePPD { get; set; }
        public string TipoDocumentoCondutor { get; set; }
        public string UFCNH { get; set; }
        public string UfPlaca { get; set; }
        public string VeiculoOutros { get; set; }
        public string VeiculoRemovido { get; set; }
        public string VeiculoRetido { get; set; }

        #endregion Public Properties
    }
}