namespace Talonario.Api.Server.Application.Entities
{
    /// <summary>
    /// [Table("inf_TipoInstrumentoMarcaModelo")]
    /// [Table("Inf_TipoInstrumento")]
    /// [Table("Inf_TipoMedicao")]
    /// </summary>
    public class EquipamentoEntity
    {
        #region Public Constructors

        public EquipamentoEntity()
        {
        }

        public EquipamentoEntity(
            int idMarcaModeloInstrumento,
            int codigoEquipamento,
            string equipamento,
            string marca,
            string unidadeDeMedida,
            int? codigoInfracao
        )
        {
            IdMarcaModeloInstrumento = idMarcaModeloInstrumento;
            CodigoEquipamento = codigoEquipamento;
            Equipamento = equipamento;
            Marca = marca;
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

        public string UnidadeDeMedida { get; set; }

        #endregion Public Properties
    }
}