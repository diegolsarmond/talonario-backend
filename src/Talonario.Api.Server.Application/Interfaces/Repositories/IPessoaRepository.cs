using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        #region Public Methods

        Task<int> InserirPessoaAbordada(PessoaAbordadaEntity veiculoAbordado);

        Task<PessoaAbordadaEntity> ObterPessoaAbordadaPorIdPessoa(string idPessoa);

        Task<IEnumerable<PessoaAbordadaEntity>> ObterPessoasAbordadas();

        Task<PessoaEntity> PesquisarExternaPorCPF(string cpf);

        Task<PessoaEntity> PesquisarPorCPF(string cpf);

        Task<bool> RemoverPessoaAbordadaPorIdPessoa(string idPessoa);

        #endregion Public Methods
    }
}