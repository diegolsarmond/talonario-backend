namespace Talonario.Api.Server.Application.Entities
{
    public class VeiculoEntity
    {
        #region Public Properties

        public string Categoria { get; set; }
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public string Especie { get; set; }
        public string EstadoEmplacamento { get; set; }
        public bool FurtadoOuRoubado { get; set; }
        public string MarcaModelo { get; set; }
        public string MunicipioEmplacamento { get; set; }
        public string PaisDoVeiculo { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public int TotalAutuacoes { get; set; }
        public int TotalMultas { get; set; }

        #endregion Public Properties
    }
}