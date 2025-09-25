using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IUsuarioApplicationService
    {
        #region Public Methods

        Task<IEnumerable<UsuarioViewModel>> LoginPorCredenciais(UsuarioLoginInputModel usuarioLoginInput);

        Task<bool> Logout(UsuarioLogout usuarioLogout);

        Task<IEnumerable<UsuarioViewModel>> ObterTodos();

        Task<bool> PodeAssinar(string matricula);

        #endregion Public Methods
    }
}