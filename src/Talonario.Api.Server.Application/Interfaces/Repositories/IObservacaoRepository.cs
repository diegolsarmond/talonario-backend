using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IObservacaoRepository
    {
        #region Public Methods

        Task<IEnumerable<ObservacaoEntity>> GetAllAtivos();

        #endregion Public Methods
    }
}