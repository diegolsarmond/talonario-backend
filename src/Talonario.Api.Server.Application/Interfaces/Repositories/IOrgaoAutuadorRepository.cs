using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IOrgaoAutuadorRepository
    {
        #region Public Methods

        Task<IEnumerable<OrgaoAutuadorEntity>> ObterOrgaoAutuadorPorEmpresa(
            List<int> empresaCodigoOrgao);

        #endregion Public Methods
    }
}