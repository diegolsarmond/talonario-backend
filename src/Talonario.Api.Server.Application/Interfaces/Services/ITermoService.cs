using System.Collections.Generic;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface ITermoService
    {
        TermoConstatacao CadastrarTermo(TermoConstatacao termo);

        TermoConstatacao CadastrarTermoFromMobile(TermoConstatacaoInputDto termoInput);

        TermoConstatacao ConsultarTermo(string numeroTc);

        IEnumerable<TermoConstatacao> ListarTermos();

        IEnumerable<TermoConstatacao> PesquisarTermos(string pesquisa);

        bool AssinarTermo(string numeroTc, string matricula);

        bool AdicionarObservacao(string numeroTc, string observacao);

        TermoConstatacao EditarTermo(TermoConstatacao termo);

        bool ExcluirTermo(string numeroTc);

        bool CancelarTermo(string numeroTc, string matricula);

        int UploadComprovante(int idTermo, int comprovante);

        bool RemoverComprovante(int idTermo);

        TermoConstatacao ObterComprovante(int idTermo);
    }
}