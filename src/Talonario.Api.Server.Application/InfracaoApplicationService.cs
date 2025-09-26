using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class InfracaoApplicationService : IInfracaoApplicationService
    {
        #region Private Fields

        private readonly IInfracaoRepository _infracaoRepository;

        #endregion Private Fields

        #region Public Constructors

        public InfracaoApplicationService(IInfracaoRepository infracaoRepository)
        {
            _infracaoRepository = infracaoRepository;
        }

        #endregion Public Constructors

        #region Private Methods

        private InfracaoEntity InfracaoViewModelToInfracaoEntity(InfracaoViewModel infracao)
        {
            return new InfracaoEntity
            {
                Placa = infracao.Placa,
                UfPlaca = infracao.UfPlaca,
                Chassi = infracao.Chassi,
                CodLocalVeiculo = infracao.CodLocalVeiculo,
                CodigoOrgaoAutuador = infracao.CodigoOrgaoAutuador,
                CodigoInfracao = infracao.CodigoInfracao,
                Desdobramento = infracao.Desdobramento,
                NumeroAuto = infracao.NumeroAuto,
                DataAuto = infracao.DataInfracao.ToShortDateString(),
                HoraAuto = infracao.DataInfracao.ToShortTimeString(),
                CodigoMunicipio = infracao.CodigoMunicipio,
                DataLote = DateTime.Now.ToShortDateString(),
                NumeroTama = string.Empty,
                NumeroTermoConstatacao = string.Empty,
                RecolhePPD = infracao.RecolhePPD,
                RecolheCNH = infracao.RecolheCNH,
                RecolheACC = infracao.RecolheACC,
                RecolheCLRV = infracao.RecolheCLRV,
                RecolheCRV = infracao.RecolheCRV,
                RecolheOutros = "N",
                RecolheOutrosDados = "N",
                VeiculoRemovido = infracao.VeiculoRemovido,
                VeiculoRetido = infracao.VeiculoRetido,
                VeiculoOutros = infracao.VeiculoOutros,
                VeiculoOutrosDados = string.Empty,
                CodigoTipoInstrumento = infracao.CodigoTipoInstrumento,
                InstrumentoAfericao = infracao.InstrumentoAfericao,
                idMarcaModeloInstrumento = 0,
                NumeroSerieInstrumento = string.Empty,
                NumeroTesteInstrumento = string.Empty,
                DataAfericao = string.Empty,
                MedicaoRegistrada = !String.IsNullOrWhiteSpace(infracao.MedicaoReal) && infracao.MedicaoReal != "0" ? infracao.MedicaoReal.Replace(",", ".") : string.Empty,
                MedicaoPermitida = !String.IsNullOrWhiteSpace(infracao.MedicaoConsiderada) ? infracao.MedicaoConsiderada.Replace(",", ".") : "0",
                CpfAgente = infracao.CpfAgente,
                MatriculaAgente = infracao.MatriculaAgente,
                Observacao = infracao.Observacao,
                NomeAgente = infracao.NomeAgente,
                Local = infracao.Local,
                NumeroLocal = "0000",
                Complemento = string.Empty,
                BairroInfracao = string.Empty,
                IndicadorAssinatura = infracao.IndicadorAssinatura.HasValue ? infracao.IndicadorAssinatura.Value : 0,
                NomeCondutor = !String.IsNullOrWhiteSpace(infracao.NomeCondutor) ? infracao.NomeCondutor : string.Empty,
                CPFCondutor = !String.IsNullOrWhiteSpace(infracao.CPFCondutor) ? infracao.CPFCondutor : string.Empty,
                TipoDocumentoCondutor = !String.IsNullOrWhiteSpace(infracao.TipoDocumentoCondutor) ? infracao.TipoDocumentoCondutor : string.Empty,
                NumeroCNH = !String.IsNullOrWhiteSpace(infracao.NumeroCNH) ? infracao.NumeroCNH : string.Empty,
                UFCNH = !String.IsNullOrWhiteSpace(infracao.UFCNH) ? infracao.UFCNH : string.Empty,
                //Abordagem = infracao.Abordagem ? "S" : "N"
            };
        }

        #endregion Private Methods

        public Task<bool> AtualizarMotivoProcessamento(string AIT, string MotivoProcessamento)
        {
            return _infracaoRepository.AtualizarMotivoProcessamento(AIT, MotivoProcessamento);
        }

        public bool AtualizarMotivoProcessamento2(string AIT, string MotivoProcessamento)
        {
            return _infracaoRepository.AtualizarMotivoProcessamento2(AIT, MotivoProcessamento);
        }

        public async Task<string> InserirInfracao(InfracaoViewModel infracao)
        {
            #region TESTE para verificar o que está vindo (LOG)

            string jsonString = JsonSerializer.Serialize(infracao);
            await _infracaoRepository.InserirInfracaoAnexo("LOG", jsonString);

            #endregion TESTE para verificar o que está vindo (LOG)

            #region Verifica se AIT válido

            if (String.IsNullOrEmpty(infracao.NumeroAuto) ||
                infracao.NumeroAuto.Length != 10)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto inválido.");

            int anoCorrente = Int32.Parse(DateTime.Now.ToString("yy"));
            int ano = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(0, 2), out ano);

            if (ano != anoCorrente && (ano < (anoCorrente - 1) || ano > (anoCorrente)))
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'Ano' inválido.");

            int IdTalonarioDispositivo = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(2, 4), out IdTalonarioDispositivo);

            if (IdTalonarioDispositivo == 0)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'IdTalonarioDispositivo' inválido.");

            int sequencia = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(6, 4), out sequencia);

            if (sequencia == 0)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'Sequencia' inválida.");

            #endregion Verifica se AIT válido

            #region Remove caracteres especiais do número

            infracao.Placa = infracao.Placa?.Replace("-", "");
            infracao.CpfAgente = infracao.CpfAgente?.Replace("-", "").Replace(".", "");
            infracao.CPFCondutor = infracao.CPFCondutor?.Replace("-", "").Replace(".", "");
            infracao.NumeroCNH = infracao.NumeroCNH?.Replace("-", "").Replace(".", "");

            #endregion Remove caracteres especiais do número

            #region Define N como default

            infracao.RecolheACC = string.IsNullOrEmpty(infracao.RecolheACC) ? "N" : infracao.RecolheACC;
            infracao.RecolheCLRV = string.IsNullOrEmpty(infracao.RecolheCLRV) ? "N" : infracao.RecolheCLRV;
            infracao.RecolheCNH = string.IsNullOrEmpty(infracao.RecolheCNH) ? "N" : infracao.RecolheCNH;
            infracao.RecolheCRV = string.IsNullOrEmpty(infracao.RecolheCRV) ? "N" : infracao.RecolheCRV;
            infracao.RecolhePPD = string.IsNullOrEmpty(infracao.RecolhePPD) ? "N" : infracao.RecolhePPD;
            //infracao.Abordagem = string.IsNullOrEmpty(infracao.Abordagem) ? "N" : infracao.Abordagem;

            #endregion Define N como default

            var infracaoEntity = InfracaoViewModelToInfracaoEntity(infracao);
            var resultInfracao = await _infracaoRepository.stp_Inf_Ws_AutoInfracaoEletronica_ins(infracaoEntity);

            if (resultInfracao == "SUCESSO")
            {
                await _infracaoRepository.AtualizarInfracaoUsuarioDispositivo(IdTalonarioDispositivo.ToString(), sequencia.ToString());
            }

            return resultInfracao;
        }

        public string InserirInfracao2(InfracaoViewModel infracao)
        {
            #region TESTE para verificar o que está vindo (LOG)

            string jsonString = JsonSerializer.Serialize(infracao);
            _infracaoRepository.InserirInfracaoAnexo2("LOG", jsonString);

            #endregion TESTE para verificar o que está vindo (LOG)

            #region Verifica se AIT válido

            if (String.IsNullOrEmpty(infracao.NumeroAuto) ||
                infracao.NumeroAuto.Length != 10)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto inválido.");

            int anoCorrente = Int32.Parse(DateTime.Now.ToString("yy"));
            int ano = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(0, 2), out ano);

            if (ano != anoCorrente && (ano < (anoCorrente - 1) || ano > (anoCorrente)))
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'Ano' inválido.");

            int IdTalonarioDispositivo = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(2, 4), out IdTalonarioDispositivo);

            if (IdTalonarioDispositivo == 0)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'IdTalonarioDispositivo' inválido.");

            int sequencia = 0;
            Int32.TryParse(infracao.NumeroAuto.Substring(6, 4), out sequencia);

            if (sequencia == 0)
                throw new ArgumentException(paramName: nameof(infracao.NumeroAuto), message: "NumeroAuto 'Sequencia' inválida.");

            #endregion Verifica se AIT válido

            #region Remove caracteres especiais do número

            infracao.Placa = infracao.Placa?.Replace("-", "");
            infracao.CpfAgente = infracao.CpfAgente?.Replace("-", "").Replace(".", "");
            infracao.CPFCondutor = infracao.CPFCondutor?.Replace("-", "").Replace(".", "");
            infracao.NumeroCNH = infracao.NumeroCNH?.Replace("-", "").Replace(".", "");

            #endregion Remove caracteres especiais do número

            #region Define N como default

            infracao.RecolheACC = string.IsNullOrEmpty(infracao.RecolheACC) ? "N" : infracao.RecolheACC;
            infracao.RecolheCLRV = string.IsNullOrEmpty(infracao.RecolheCLRV) ? "N" : infracao.RecolheCLRV;
            infracao.RecolheCNH = string.IsNullOrEmpty(infracao.RecolheCNH) ? "N" : infracao.RecolheCNH;
            infracao.RecolheCRV = string.IsNullOrEmpty(infracao.RecolheCRV) ? "N" : infracao.RecolheCRV;
            infracao.RecolhePPD = string.IsNullOrEmpty(infracao.RecolhePPD) ? "N" : infracao.RecolhePPD;
            //infracao.Abordagem = string.IsNullOrEmpty(infracao.Abordagem) ? "N" : infracao.Abordagem;

            #endregion Define N como default

            var infracaoEntity = InfracaoViewModelToInfracaoEntity(infracao);
            var resultInfracao = _infracaoRepository.stp_Inf_Ws_AutoInfracaoEletronica_ins2(infracaoEntity);

            if (resultInfracao == "SUCESSO")
            {
                _infracaoRepository.AtualizarInfracaoUsuarioDispositivo2(IdTalonarioDispositivo.ToString(), sequencia.ToString());
            }

            return resultInfracao;
        }

        public async Task<int> InserirInfracaoAnexo(InfracaoAnexoInputModel infracaoAnexo)
        {
            if (string.IsNullOrEmpty(infracaoAnexo.AIT))
                throw new ArgumentException(paramName: nameof(infracaoAnexo.AIT), message: "AIT da Infração inválido.");

            var idUltimoInserido = 0;
            await _infracaoRepository.RemoverInfracaoAnexo(infracaoAnexo.AIT);

            if (infracaoAnexo.AnexoBase64 != null)
            {
                foreach (var anexoBase64 in infracaoAnexo.AnexoBase64)
                {
                    idUltimoInserido = await _infracaoRepository.InserirInfracaoAnexo(infracaoAnexo.AIT, anexoBase64);
                }
            }

            return idUltimoInserido;
        }

        public async Task<int> InserirInfracaoNaoTransmitida(InfracaoNaoTransmitidaViewModel infracaoNaoTransmitida)
        {
            infracaoNaoTransmitida.AIT = infracaoNaoTransmitida.AIT.Trim();

            var linhasAtualizadas = await _infracaoRepository.AtualizarInfracaoNaoTransmitidaAsync(infracaoNaoTransmitida);

            if (linhasAtualizadas > 0)
                return linhasAtualizadas;

            var idInfracaoInserida = await _infracaoRepository.InserirInfracaoNaoTransmitida(infracaoNaoTransmitida);
            return idInfracaoInserida;
        }

        public async Task<int> InserirInfracaoPdf(InfracaoPdfInputModel infracaoPdf)
        {
            if (string.IsNullOrEmpty(infracaoPdf.AIT))
                throw new ArgumentException(paramName: nameof(infracaoPdf.AIT), message: "AIT da Infração inválido.");

            if (string.IsNullOrEmpty(infracaoPdf.PdfBase64))
                throw new ArgumentException(paramName: nameof(infracaoPdf.PdfBase64), message: "PDF da Infração inválido.");

            await _infracaoRepository.RemoverInfracaoPdf(infracaoPdf.AIT);

            return await _infracaoRepository.InserirInfracaoPdf(infracaoPdf.AIT, infracaoPdf.PdfBase64);
        }

        public async Task<int> InserirInfracaoPessoa(InfracaoPessoaViewModel infracaoPessoa)
        {
            #region Verifica se AIT válido

            if (String.IsNullOrEmpty(infracaoPessoa.NumeroAuto) ||
                infracaoPessoa.NumeroAuto.Length != 10)
                throw new ArgumentException(paramName: nameof(infracaoPessoa.NumeroAuto), message: "NumeroAuto inválido.");

            int anoCorrente = Int32.Parse(DateTime.Now.ToString("yy"));
            int ano = 0;
            Int32.TryParse(infracaoPessoa.NumeroAuto.Substring(0, 2), out ano);

            if (ano != anoCorrente && (ano < (anoCorrente - 1) || ano > (anoCorrente)))
                throw new ArgumentException(paramName: nameof(infracaoPessoa.NumeroAuto), message: "NumeroAuto 'Ano' inválido.");

            int IdTalonarioDispositivo = 0;
            Int32.TryParse(infracaoPessoa.NumeroAuto.Substring(2, 4), out IdTalonarioDispositivo);

            if (IdTalonarioDispositivo == 0)
                throw new ArgumentException(paramName: nameof(infracaoPessoa.NumeroAuto), message: "NumeroAuto 'IdTalonarioDispositivo' inválido.");

            int sequencia = 0;
            Int32.TryParse(infracaoPessoa.NumeroAuto.Substring(6, 4), out sequencia);

            if (sequencia == 0)
                throw new ArgumentException(paramName: nameof(infracaoPessoa.NumeroAuto), message: "NumeroAuto 'Sequencia' inválida.");

            #endregion Verifica se AIT válido

            var resultInfracao = await _infracaoRepository.InserirInfracaoPessoa(infracaoPessoa);

            await _infracaoRepository.AtualizarInfracaoUsuarioDispositivo(IdTalonarioDispositivo.ToString(), sequencia.ToString());

            return resultInfracao;
        }

        public async Task<IEnumerable<TipoInfracaoViewModel>> ObterDicionarioDeTiposDeInfracoes()
        {
            var infracoesModel = new List<TipoInfracaoViewModel>();

            var infracoes = await _infracaoRepository.ObterDicionarioDeTiposDeInfracoes();
            infracoes.ToList()?.ForEach(infracao =>
                infracoesModel.Add(
                    InfracaoViewModelMapper.TipoInfracaoMapper(infracao)
                )
            );

            return infracoesModel;
        }

        public async Task<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>> ObterEquipamentosDeRegistroDeInfracoes()
        {
            var equipamentosModel = new List<EquipamentoDeRegistroDeInfracaoViewModel>();

            var equipamentos = await _infracaoRepository.ObterEquipamentosDeRegistroDeInfracoes();
            equipamentos.ToList()?.ForEach(equipamento =>
                equipamentosModel.Add(
                    EquipamentoViewModelMapper.EquipamentoDeRegistroDeInfracaoMapper(equipamento)
                )
            );

            return equipamentosModel;
        }

        public async Task<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>> ObterEquipamentosDeRegistroDeInfracoesPorInfracao(int codigoInfracao)
        {
            var equipamentosModel = new List<EquipamentoDeRegistroDeInfracaoViewModel>();

            var equipamentos = await _infracaoRepository.ObterEquipamentosDeRegistroDeInfracoesPorInfracao(codigoInfracao);
            equipamentos.ToList()?.ForEach(equipamento =>
                equipamentosModel.Add(
                    EquipamentoViewModelMapper.EquipamentoDeRegistroDeInfracaoMapper(equipamento)
                )
            );

            return equipamentosModel;
        }

        public async Task<IEnumerable<InfracaoAnexoViewModel>> ObterInfracaoAnexosPorIdInfracao(string ait)
        {
            var infracaoAnexos = new List<InfracaoAnexoViewModel>();

            var anexos = await _infracaoRepository.ObterInfracaoAnexosPorIdInfracao(ait);
            anexos.ToList()?.ForEach(infracaoAnexo =>
                infracaoAnexos.Add(
                    InfracaoAnexoViewModelMapper.InfracaoAnexoMapper(infracaoAnexo)
                )
            );

            return infracaoAnexos;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidas()
        {
            var infracoesModel = new List<InfracaoNaoTransmitidaViewModel>();

            var infracoesNaoTransmitidas = await _infracaoRepository.ObterInfracoesNaoTransmitidas();
            foreach (var infracaoNaoTransmitida in infracoesNaoTransmitidas)
            {
                infracoesModel.Add(InfracaoNaoTransmitidaViewModelMapper.TipoInfracaoNaoTransmitidaMapper(infracaoNaoTransmitida));
            }

            return infracoesModel;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidasComChassiSemPlaca()
        {
            var infracoesModel = new List<InfracaoNaoTransmitidaViewModel>();

            var infracoesNaoTransmitidasComChassiSemPlaca = await _infracaoRepository.ObterInfracoesNaoTransmitidasComChassiSemPlaca();

            foreach (var infracaoNaoTransmitida in infracoesNaoTransmitidasComChassiSemPlaca)
                infracoesModel.Add(InfracaoNaoTransmitidaViewModelMapper.TipoInfracaoNaoTransmitidaMapper(infracaoNaoTransmitida));

            return infracoesModel;
        }

        public IEnumerable<InfracaoNaoTransmitidaViewModel> ObterInfracoesNaoTransmitidasComChassiSemPlaca2()
        {
            var infracoesModel = new List<InfracaoNaoTransmitidaViewModel>();

            var infracoesNaoTransmitidasComChassiSemPlaca = _infracaoRepository.ObterInfracoesNaoTransmitidasComChassiSemPlaca2();

            foreach (var infracaoNaoTransmitida in infracoesNaoTransmitidasComChassiSemPlaca)
                infracoesModel.Add(InfracaoNaoTransmitidaViewModelMapper.TipoInfracaoNaoTransmitidaMapper(infracaoNaoTransmitida));

            return infracoesModel;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaViewModel>> ObterInfracoesNaoTransmitidasPessoas()
        {
            var infracoesModel = new List<InfracaoNaoTransmitidaViewModel>();

            var infracoesNaoTransmitidas = await _infracaoRepository.ObterInfracoesNaoTransmitidasPessoas();
            foreach (var infracaoNaoTransmitida in infracoesNaoTransmitidas)
            {
                infracoesModel.Add(InfracaoNaoTransmitidaViewModelMapper.TipoInfracaoNaoTransmitidaMapper(infracaoNaoTransmitida));
            }

            return infracoesModel;
        }

        public async Task<VeiculoEmplacadoViewModel> ObterPlacaPorChassi(string chassi)
        {
            if (string.IsNullOrEmpty(chassi)) return null;

            return await _infracaoRepository.ObterPlacaPorChassi(chassi);
        }

        public VeiculoEmplacadoViewModel ObterPlacaPorChassi2(string chassi)
        {
            if (string.IsNullOrEmpty(chassi)) return null;

            return _infracaoRepository.ObterPlacaPorChassi2(chassi);
        }
    }
}