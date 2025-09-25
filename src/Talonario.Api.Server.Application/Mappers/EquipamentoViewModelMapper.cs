using System.Linq;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class EquipamentoViewModelMapper
    {
        #region Public Methods

        public static EquipamentoDeRegistroDeInfracaoViewModel EquipamentoDeRegistroDeInfracaoMapper(EquipamentoEntity equipamentoEntity)
        {
            string separador = equipamentoEntity.Marca?.Contains("\\") == true ? "\\" : "/";
            var marcaModelo = equipamentoEntity.Marca?.Split(separador);

            string marca = (marcaModelo?.Count() > 0) ? marcaModelo[0] : equipamentoEntity.Marca;
            string modelo = (marcaModelo?.Count() > 1) ? marcaModelo[1] : "";

            return new EquipamentoDeRegistroDeInfracaoViewModel(
                equipamentoEntity.IdMarcaModeloInstrumento,
                equipamentoEntity.CodigoEquipamento,
                equipamentoEntity.Equipamento,
                marca,
                modelo,
                equipamentoEntity.UnidadeDeMedida,
                equipamentoEntity.CodigoInfracao
            );
        }

        #endregion Public Methods
    }
}