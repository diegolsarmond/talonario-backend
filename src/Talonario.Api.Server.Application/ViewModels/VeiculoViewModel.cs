using Talonario.Api.Server.Application.Enums;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class Mensagem
    {
        #region Public Properties

        public string Conteudo { get; set; }
        public TipoMensagem Tipo { get; set; }

        #endregion Public Properties
    }

    public class VeiculoViewModel
    {
        #region Public Properties

        public string Categoria { get; set; }
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public string Especie { get; set; }
        public string EstadoEmplacamento { get; set; }
        public string MarcaModelo { get; set; }
        public Mensagem Mensagem { get; set; }
        public string MunicipioEmplacamento { get; set; }
        public string PaisDoVeiculo { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }

        #endregion Public Properties
    }
}