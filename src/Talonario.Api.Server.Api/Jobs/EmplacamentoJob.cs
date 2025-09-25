using Newtonsoft.Json;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Api.Jobs
{
    /// <summary>
    /// EmplacamentoJob
    /// </summary>
    public class EmplacamentoJob : IHostedService
    {
        #region Private Fields

        private readonly IInfracaoApplicationService _infracaoApplicationService;

        private Timer? _timer;

        private int minutos_1 = 1000 * 10 * 1;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="infracaoApplicationService"></param>
        public EmplacamentoJob(IServiceProvider serviceProvider, IInfracaoApplicationService infracaoApplicationService)
        {
            ServiceProvider = serviceProvider;
            this._infracaoApplicationService = infracaoApplicationService;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// ServiceProvider
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Inicia Service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(VerificaEAtualizaAitsSemPlaca, null, 0, minutos_1);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Encerra Service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        /// <summary>
        /// Verifica AITs sem Placa, atualiza JSON, processa infração e atualiza status (JSON e não transmitidas)
        /// </summary>
        /// <param name="state"></param>
        public void VerificaEAtualizaAitsSemPlaca(object? state)
        {
            try
            {
                // 1 - Busca infrações não transmitidas com chassi e sem placa

                var infracoesNaoTransmitidasComChassiSemPlaca = _infracaoApplicationService.ObterInfracoesNaoTransmitidasComChassiSemPlaca2();

                // 2 - Verifica se veículo foi emplacado

                var infracoes = new List<InfracaoViewModel>();

                foreach (var infracaoNaoTransmitida in infracoesNaoTransmitidasComChassiSemPlaca)
                {
                    if (infracaoNaoTransmitida == null) continue;

                    var infracaoNaoTransmitidaJSON = JsonConvert.DeserializeObject<InfracaoNaoTransmitidaJSON>(infracaoNaoTransmitida.JSON);

                    if (infracaoNaoTransmitidaJSON == null) continue;

                    var chassi = infracaoNaoTransmitidaJSON?.veiculoChassi;

                    var veiculoEmplacado = _infracaoApplicationService.ObterPlacaPorChassi2(chassi);

                    if (veiculoEmplacado == null || string.IsNullOrEmpty(veiculoEmplacado.Placa)) continue;

                    var infracao = new InfracaoViewModel();
                    infracao.Placa = veiculoEmplacado.Placa;
                    infracao.Chassi = chassi;
                    infracao.Abordagem = infracaoNaoTransmitidaJSON?.condutorFoiAbordado;
                    infracao.CodigoInfracao = infracaoNaoTransmitidaJSON?.artigoCodigo;
                    infracao.CodigoMunicipio = infracaoNaoTransmitidaJSON?.codigoMunicipio;
                    infracao.CodigoOrgaoAutuador = Convert.ToInt32(infracaoNaoTransmitidaJSON?.idOrgaoAplicador);
                    infracao.CodigoTipoInstrumento = infracaoNaoTransmitidaJSON?.equipamentoUsado;
                    //CodLocalVeiculo = infracaoNaoTransmitidaJSON?.localId;
                    infracao.CpfAgente = infracaoNaoTransmitidaJSON?.cpfAssinante;

                    infracao.TipoDocumentoCondutor = infracaoNaoTransmitidaJSON?.condutorDocumentoTipo;
                    infracao.CPFCondutor = infracaoNaoTransmitidaJSON?.condutorDocumentoNumero;
                    infracao.NumeroCNH = infracaoNaoTransmitidaJSON?.cnhNumero;

                    infracao.DataInfracao = Convert.ToDateTime(infracaoNaoTransmitidaJSON?.dataAplicacao);
                    infracao.Desdobramento = infracaoNaoTransmitidaJSON?.artigoDesdobramento;
                    infracao.IndicadorAssinatura = 0;
                    infracao.InstrumentoAfericao = infracaoNaoTransmitidaJSON?.equipamentoNome;
                    infracao.Local = infracaoNaoTransmitidaJSON?.localRua + ", " +
                        infracaoNaoTransmitidaJSON?.localNumero + " - " +
                        infracaoNaoTransmitidaJSON?.localBairro + " - " +
                        infracaoNaoTransmitidaJSON?.localCidade + "/" +
                        infracaoNaoTransmitidaJSON?.localEstado + " - " +
                        infracaoNaoTransmitidaJSON?.localCEP;
                    infracao.MatriculaAgente = string.Empty;
                    infracao.MedicaoConsiderada = infracaoNaoTransmitidaJSON?.equipamentoConsiderado;
                    infracao.MedicaoReal = infracaoNaoTransmitidaJSON?.equipamentoDetectado;
                    infracao.NomeAgente = infracaoNaoTransmitidaJSON?.nomeAssinante;
                    infracao.NomeCondutor = infracaoNaoTransmitidaJSON?.condutorNome;
                    infracao.NumeroAuto = infracaoNaoTransmitidaJSON?.id;
                    infracao.Observacao = infracaoNaoTransmitidaJSON?.observacoes;
                    infracao.RecolheACC = "N";
                    infracao.RecolheCLRV = "N";
                    infracao.RecolheCRV = "N";
                    infracao.RecolhePPD = "N";
                    infracao.RecolheCNH = infracaoNaoTransmitidaJSON?.articleSuspendCNH;
                    infracao.UFCNH = infracaoNaoTransmitidaJSON?.cnhEstado;
                    infracao.UfPlaca = infracaoNaoTransmitidaJSON?.veiculoEmplEstado;
                    infracao.VeiculoOutros = string.Empty;
                    infracao.VeiculoRemovido = infracaoNaoTransmitidaJSON?.artigoRemoverVeiculo;
                    infracao.VeiculoRetido = infracaoNaoTransmitidaJSON?.artigoApreensaoVeiculo;

                    infracoes.Add(infracao);
                }

                #region 3 - Insere a infração

                foreach (var infracao in infracoes)
                {
                    var result = _infracaoApplicationService.InserirInfracao2(infracao);

                    _infracaoApplicationService.AtualizarMotivoProcessamento2(infracao.NumeroAuto, result);
                }

                #endregion 3 - Insere a infração
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        #endregion Public Methods
    }
}