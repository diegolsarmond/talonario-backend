namespace Talonario.Api.Server.Application.ViewModels
{
    public class EquipamentoDeRegistroDeInfracaoViewModel
    {
        #region Public Constructors

        public EquipamentoDeRegistroDeInfracaoViewModel()
        {
        }

        public EquipamentoDeRegistroDeInfracaoViewModel(
            int idMarcaModeloInstrumento,
            int codigoEquipamento,
            string equipamento,
            string marca,
            string modelo,
            string unidadeDeMedida,
            int? codigoInfracao
        )
        {
            IdMarcaModeloInstrumento = idMarcaModeloInstrumento;
            CodigoEquipamento = codigoEquipamento;
            Equipamento = equipamento;
            Marca = marca;
            Modelo = modelo;
            UnidadeDeMedida = unidadeDeMedida;
            CodigoInfracao = codigoInfracao;
        }

        #endregion Public Constructors

        #region Public Properties

        public int CodigoEquipamento { get; set; }

        public int? CodigoInfracao { get; set; }

        public string Equipamento { get; set; }

        public int IdMarcaModeloInstrumento { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string UnidadeDeMedida { get; set; }

        #endregion Public Properties
    }
}