using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ICampanhasTalonarioRepository
    {
        #region Public Methods

        Task<int> Adicionar(InfCampanhasTalonario campanha);

        Task<IEnumerable<InfCampanhasTalonario>> ObterAtivo();

        Task<IEnumerable<InfCampanhasTalonario>> ObterInativas();

        #endregion Public Methods
    }
}