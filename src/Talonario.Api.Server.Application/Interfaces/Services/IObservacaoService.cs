using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IObservacaoService
    {
        #region Public Methods

        Task<IEnumerable<ObservacaoViewModel>> GetAllAtivos();

        #endregion Public Methods
    }
}