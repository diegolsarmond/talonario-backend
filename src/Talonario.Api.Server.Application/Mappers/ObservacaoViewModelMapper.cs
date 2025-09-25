using System.Collections.Generic;
using System.Linq;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class ObservacaoViewModelMapper
    {
        #region Public Methods

        public static ObservacaoViewModel ObservacaoMapper(ObservacaoEntity observacaoEntity)
        {
            if (observacaoEntity is null)
            {
                return null;
            }

            return new(observacaoEntity.Id, observacaoEntity.Titulo, observacaoEntity.Descricao);
        }

        public static IEnumerable<ObservacaoViewModel> ObservacaoMapper(IEnumerable<ObservacaoEntity> listaObservacoesEntity)
        {
            if (listaObservacoesEntity is null)
            {
                return null;
            }

            return listaObservacoesEntity.Select(o => new ObservacaoViewModel(o.Id, o.Titulo, o.Descricao)).ToList();
        }

        #endregion Public Methods
    }
}