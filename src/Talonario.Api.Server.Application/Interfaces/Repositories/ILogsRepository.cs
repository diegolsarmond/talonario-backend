using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ILogsRepository
    {
        Task InserirLoteAsync(List<RegistroLogTalonarioEntity> entity);
    }
}