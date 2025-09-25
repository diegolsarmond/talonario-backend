using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IPessoaApplicationService
    {
        #region Public Methods

        Task<int> InserirPessoaAbordada(PessoaAbordadaViewModel pessoaAbordadaModel);

        Task<PessoaAbordadaViewModel> ObterPessoaAbordadaPorIdPessoa(string idPessoa);

        Task<IEnumerable<PessoaAbordadaViewModel>> ObterPessoasAbordadas();

        Task<PessoaViewModel> PesquisarExternaPorCPF(string cpf);

        Task<PessoaViewModel> PesquisarPorCPF(string cpf);

        Task<bool> RemoverPessoaAbordadaPorIdPessoa(string idPessoa);

        #endregion Public Methods
    }
}