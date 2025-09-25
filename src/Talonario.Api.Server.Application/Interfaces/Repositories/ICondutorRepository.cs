using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ICondutorRepository
    {
        #region Public Methods

        Task<CondutorEntity> PesquisarExternaPorCPF(string cpf);

        Task<CondutorEntity> PesquisarPorCpf(string cpf);

        #endregion Public Methods
    }
}