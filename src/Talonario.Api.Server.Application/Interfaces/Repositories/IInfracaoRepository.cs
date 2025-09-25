using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Interfaces.Repositories
{
    public interface IInfracaoRepository
    {
        #region Public Methods

        Task<bool> AtualizarInfracaoUsuarioDispositivo(string idTalonarioDispositivo, string sequencia);

        bool AtualizarInfracaoUsuarioDispositivo2(string idTalonarioDispositivo, string sequencia);

        Task<bool> AtualizarMotivoProcessamento(string AIT, string MotivoProcessamento);

        bool AtualizarMotivoProcessamento2(string AIT, string MotivoProcessamento);

        Task<int> InserirInfracaoAnexo(string ait, string anexoBase64);

        int InserirInfracaoAnexo2(string ait, string anexoBase64);

        Task<int> InserirInfracaoNaoTransmitida(InfracaoNaoTransmitidaViewModel infracaoNaoTransmitida);

        Task<int> InserirInfracaoPdf(string ait, string pdfBase64);

        Task<int> InserirInfracaoPessoa(InfracaoPessoaViewModel infracaoPessoa);

        Task<int> InserirInfracaoUsuarioDispositivo(string idDispositivo, string sequencia);

        Task<IEnumerable<TipoInfracaoEntity>> ObterDicionarioDeTiposDeInfracoes();

        Task<IEnumerable<EquipamentoEntity>> ObterEquipamentosDeRegistroDeInfracoes();

        Task<IEnumerable<EquipamentoEntity>> ObterEquipamentosDeRegistroDeInfracoesPorInfracao(int codigoInfracao);

        Task<IEnumerable<InfracaoAnexoEntity>> ObterInfracaoAnexosPorIdInfracao(string ait);

        Task<TalonarioDispositivoEntity> ObterInfracaoUsuarioDispositivoPorIdDispositivo(string idDispositivo);

        Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidas();

        Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidasComChassiSemPlaca();

        IEnumerable<InfracaoNaoTransmitidaEntity> ObterInfracoesNaoTransmitidasComChassiSemPlaca2();

        Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidasPessoas();

        Task<VeiculoEmplacadoViewModel> ObterPlacaPorChassi(string chassi);

        VeiculoEmplacadoViewModel ObterPlacaPorChassi2(string chassi);

        Task<bool> RemoverInfracaoAnexo(string ait);

        Task<bool> RemoverInfracaoNaoTransmitidaPorAIT(string ait);

        Task<bool> RemoverInfracaoPdf(string ait);

        Task<string> stp_Inf_Ws_AutoInfracaoEletronica_ins(InfracaoEntity infracao);

        string stp_Inf_Ws_AutoInfracaoEletronica_ins2(InfracaoEntity infracao);

        #endregion Public Methods
    }
}