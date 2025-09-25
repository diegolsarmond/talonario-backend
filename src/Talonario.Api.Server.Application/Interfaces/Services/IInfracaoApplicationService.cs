using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Services
{
    public interface IInfracaoApplicationService
    {
        #region Public Methods

        Task<bool> AtualizarMotivoProcessamento(string AIT, string MotivoProcessamento);

        bool AtualizarMotivoProcessamento2(string AIT, string MotivoProcessamento);

        Task<string> InserirInfracao(InfracaoViewModel infracao);

        string InserirInfracao2(InfracaoViewModel infracao);

        Task<int> InserirInfracaoAnexo(InfracaoAnexoInputModel infracaoAnexo);

        Task<int> InserirInfracaoNaoTransmitida(InfracaoNaoTransmitidaViewModel infracaoNaoTransmitida);

        Task<int> InserirInfracaoPdf(InfracaoPdfInputModel infracaoPdf);

        Task<int> InserirInfracaoPessoa(InfracaoPessoaViewModel infracaoPessoa);

        Task<IEnumerable<TipoInfracaoViewModel>> ObterDicionarioDeTiposDeInfracoes();

        Task<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>> ObterEquipamentosDeRegistroDeInfracoes();

        Task<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>> ObterEquipamentosDeRegistroDeInfracoesPorInfracao(int codigoInfracao);

        Task<IEnumerable<InfracaoAnexoViewModel>> ObterInfracaoAnexosPorIdInfracao(string ait);

        Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidas();

        Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidasComChassiSemPlaca();

        IEnumerable<InfracaoNaoTransmitidaViewModel> ObterInfracoesNaoTransmitidasComChassiSemPlaca2();

        Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidasPessoas();

        Task<VeiculoEmplacadoViewModel> ObterPlacaPorChassi(string chassi);

        VeiculoEmplacadoViewModel ObterPlacaPorChassi2(string chassi);

        #endregion Public Methods
    }
}