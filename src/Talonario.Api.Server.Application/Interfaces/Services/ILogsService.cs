using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ILogsService
    {
        Task RegistrarLogsAsync(List<RegistroLogTalonarioViewModel> viewModel);
    }
}