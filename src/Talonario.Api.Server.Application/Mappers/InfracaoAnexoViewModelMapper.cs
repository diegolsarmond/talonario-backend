using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class InfracaoAnexoViewModelMapper
    {
        #region Public Methods

        public static InfracaoAnexoViewModel InfracaoAnexoMapper(InfracaoAnexoEntity infracaoAnexoEntity)
        {
            return new InfracaoAnexoViewModel(
                infracaoAnexoEntity.Id,
                infracaoAnexoEntity.AIT,
                infracaoAnexoEntity.AnexoBase64
            );
        }

        #endregion Public Methods
    }
}