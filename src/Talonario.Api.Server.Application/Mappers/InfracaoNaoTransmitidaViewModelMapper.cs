using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class InfracaoNaoTransmitidaViewModelMapper
    {
        #region Public Methods

        public static InfracaoNaoTransmitidaViewModel TipoInfracaoNaoTransmitidaMapper(InfracaoNaoTransmitidaEntity infracaoNaoTransmitidaEntity)
        {
            return new InfracaoNaoTransmitidaViewModel(
                infracaoNaoTransmitidaEntity.Id,
                infracaoNaoTransmitidaEntity.AIT,
                infracaoNaoTransmitidaEntity.JSON,
                infracaoNaoTransmitidaEntity.Tipo == null ? "veiculo" : infracaoNaoTransmitidaEntity.Tipo,
                infracaoNaoTransmitidaEntity.DataCancelamento,
                infracaoNaoTransmitidaEntity.DataEnviado,
                infracaoNaoTransmitidaEntity.MotivoProcessamento,
                infracaoNaoTransmitidaEntity.DataInclusao
            );
        }

        #endregion Public Methods
    }
}