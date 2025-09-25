using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class ObservacaoService : IObservacaoService
    {
        #region Private Fields

        private readonly IObservacaoRepository _observacaoRepository;

        #endregion Private Fields

        #region Public Constructors

        public ObservacaoService(IObservacaoRepository observacaoRepository)
        {
            _observacaoRepository = observacaoRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<ObservacaoViewModel>> GetAllAtivos()
        {
            var observacoes = await _observacaoRepository.GetAllAtivos();

            if (observacoes is null)
                return Enumerable.Empty<ObservacaoViewModel>();

            return ObservacaoViewModelMapper.ObservacaoMapper(observacoes);
        }

        #endregion Public Methods
    }
}