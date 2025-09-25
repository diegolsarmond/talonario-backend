using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        #region Public Methods

        Task<bool> AtualizaUsuarioLogado(string cpf, string idDispositivo);

        Task<int> InsereUsuarioLogado(string cpf, string idDispositivo);

        Task<bool> Logout(string cpf, string idDispositivo);

        Task<IEnumerable<UsuarioEntity>> ObterPorCPF(string cpf);

        Task<IEnumerable<UsuarioEntity>> ObterTodos();

        Task<bool> PodeAssinar(string matricula);

        int RetornaMatriculaAgente(int matricula);

        Task<UsuarioLogadoEntity> VerificaUsuarioLogado(string cpf);

        #endregion Public Methods
    }
}