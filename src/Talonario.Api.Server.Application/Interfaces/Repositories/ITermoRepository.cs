using System.Collections.Generic;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface ITermoRepository
    {
        TermoConstatacao CadastrarTermoConstatacao(TermoConstatacao termo);

        TermoConstatacao ConsultarTermoConstatacao(string numeroTc);

        IEnumerable<TermoConstatacao> ListarTermoConstatacao();

        IEnumerable<TermoConstatacao> PesquisarTermoConstatacao(string pesquisa);

        bool AssinarTermoConstatacao(TermoConstatacao termo);

        bool AdicionarObservacaoTermo(TermoConstatacao termo);

        TermoConstatacao EditarTermoConstatacao(TermoConstatacao termo);

        bool ExcluirTermoConstatacao(string numeroTc);

        bool CancelarTermoConstatacao(TermoConstatacao termo);

        int UploadComprovanteTermo(int idTermo, int comprovante);

        bool RemoverComprovanteTermo(int idTermo);

        TermoConstatacao ObterComprovanteTermo(int idTermo);
    }
}