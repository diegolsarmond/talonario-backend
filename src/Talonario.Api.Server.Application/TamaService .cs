using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

public class TamaService : ITamaService
{
    private readonly ITamaRepository _tamaRepository;
    private readonly ILogger<TamaService> _logger;

    public TamaService(ITamaRepository tamaRepository, ILogger<TamaService> logger)
    {
        _tamaRepository = tamaRepository;
        _logger = logger;
    }

    public async Task<TamaViewModel> CadastrarTamaAsync(TamaInputCompletoViewModel input)
    {
        try
        {
            var tamaViewModel = MapearParaViewModel(input);

            var tamaEntity = new TamaEntity();
            tamaEntity.NumeroTAMA = tamaViewModel.NumeroTAMA;
            tamaEntity.NumeroReciboRetencaoTalonario = tamaViewModel.NumeroReciboRetencaoTalonario;
            tamaEntity.CepLocalRecolhimento = tamaViewModel.CepLocalRecolhimento;
            tamaEntity.EnderecoLocalRecolhimento = tamaViewModel.EnderecoLocalRecolhimento;
            tamaEntity.MunicipioUfLocalRecolhimento = tamaViewModel.MunicipioUfLocalRecolhimento;
            tamaEntity.DataHoraLocalRecolhimento = tamaViewModel.DataHoraLocalRecolhimento;
            tamaEntity.LatitudeLocalRecolhimento = tamaViewModel.LatitudeLocalRecolhimento;
            tamaEntity.LongitudeLocalRecolhimento = tamaViewModel.LongitudeLocalRecolhimento;
            tamaEntity.TransporteLocalRecolhimento = tamaViewModel.TransporteLocalRecolhimento;
            tamaEntity.PlacaVeiculo = tamaViewModel.PlacaVeiculo;
            tamaEntity.PaisVeiculo = tamaViewModel.PaisVeiculo;
            tamaEntity.MunicipioVeiculo = tamaViewModel.MunicipioVeiculo;
            tamaEntity.UfVeiculo = tamaViewModel.UfVeiculo;
            tamaEntity.RenavamVeiculo = tamaViewModel.RenavamVeiculo;
            tamaEntity.MarcaModeloVeiculo = tamaViewModel.MarcaModeloVeiculo;
            tamaEntity.EspecieVeiculo = tamaViewModel.EspecieVeiculo;
            tamaEntity.CategoriaVeiculo = tamaViewModel.CategoriaVeiculo;
            tamaEntity.CorVeiculo = tamaViewModel.CorVeiculo;
            tamaEntity.NomeCondutor = tamaViewModel.NomeCondutor;
            tamaEntity.CpfCondutor = tamaViewModel.CpfCondutor;
            tamaEntity.RgCondutor = tamaViewModel.RgCondutor;
            tamaEntity.CnhCondutor = tamaViewModel.CnhCondutor;
            tamaEntity.TelefoneCondutor = tamaViewModel.TelefoneCondutor;
            tamaEntity.CepCondutor = tamaViewModel.CepCondutor;
            tamaEntity.EnderecoCondutor = tamaViewModel.EnderecoCondutor;
            tamaEntity.MunicipioUfCondutor = tamaViewModel.MunicipioUfCondutor;
            tamaEntity.NomeCondutorEntregue = tamaViewModel.NomeCondutorEntregue;
            tamaEntity.CpfCondutorEntregue = tamaViewModel.CpfCondutorEntregue;
            tamaEntity.RgCondutorEntregue = tamaViewModel.RgCondutorEntregue;
            tamaEntity.CnhCondutorEntregue = tamaViewModel.CnhCondutorEntregue;
            tamaEntity.TelefoneCondutorEntregue = tamaViewModel.TelefoneCondutorEntregue;
            tamaEntity.CepCondutorEntregue = tamaViewModel.CepCondutorEntregue;
            tamaEntity.EnderecoCondutorEntregue = tamaViewModel.EnderecoCondutorEntregue;
            tamaEntity.MunicipioUfCondutorEntregue = tamaViewModel.MunicipioUfCondutorEntregue;
            tamaEntity.Observacoes = tamaViewModel.Observacoes;
            tamaEntity.MatriculaAgente = tamaViewModel.MatriculaAgente;
            tamaEntity.MatriculaTestemunha1 = tamaViewModel.MatriculaTestemunha1;
            tamaEntity.MatriculaTestemunha2 = tamaViewModel.MatriculaTestemunha2;
            tamaEntity.EstadoGeralLatariaPintura = tamaViewModel.EstadoGeralLatariaPintura;
            tamaEntity.EquipamentosObrigatoriosAusentes = tamaViewModel.EquipamentosObrigatoriosAusentes;
            tamaEntity.ObjetosEncontradosVeiculo = tamaViewModel.ObjetosEncontradosVeiculo;
            tamaEntity.VeiculoEntregueComChave = tamaViewModel.VeiculoEntregueComChave;
            tamaEntity.DataHoraInclusao = tamaViewModel.DataHoraInclusao;
            tamaEntity.UsuarioInclusao = tamaViewModel.UsuarioInclusao;
            tamaEntity.DataHoraAssinou = tamaViewModel.DataHoraAssinou;
            tamaEntity.MatriculaAssinou = tamaViewModel.MatriculaAssinou;
            tamaEntity.DataHoraCancelou = tamaViewModel.DataHoraCancelou;
            tamaEntity.MatriculaCancelou = tamaViewModel.MatriculaCancelou;
            tamaEntity.CodigoVerificador = tamaViewModel.CodigoVerificador;
            tamaEntity.ObservacoesAdicionais = tamaViewModel.ObservacoesAdicionais;
            tamaEntity.NumeroCondutor = tamaViewModel.NumeroCondutor;
            tamaEntity.BairroCondutor = tamaViewModel.BairroCondutor;
            tamaEntity.NumeroLocalInfracao = tamaViewModel.NumeroLocalInfracao;
            tamaEntity.BairroLocalInfracao = tamaViewModel.BairroLocalInfracao;
            tamaEntity.Chassi = tamaViewModel.Chassi;
            tamaEntity.VeiculoEmplacado = tamaViewModel.VeiculoEmplacado;

            tamaEntity.AutosInfracao = tamaViewModel.AutosInfracao?.Select(ai => new TermoAdocaoMedidaAdministrativa_AutosInfracao
            {
                Numero = ai.Numero,
                Tipo = ai.Tipo
            }).ToList();

            tamaEntity.DocumentosRecolhidos = tamaViewModel.DocumentosRecolhidos?.Select(dr => new TermoAdocaoMedidaAdministrativa_DocumentosRecolhidos
            {
                Documento = dr.Documento,
                Numero = dr.Numero
            }).ToList();

            tamaEntity.EquipamentosObrigatoriosAusentesLista = tamaViewModel.EquipamentosAusentes?.Select(ea => new TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentes
            {
                IdEquipamentoObrigatorio = ea.IdEquipamentoObrigatorio,
                EquipamentoObrigatorio = ea.EquipamentoObrigatorio
            }).ToList();

            await _tamaRepository.CadastrarTermoAdocaoMedidaAdministrativaAsync(tamaEntity);

            _logger.LogInformation("TAMA cadastrado com sucesso: {TamaId}", tamaViewModel.NumeroTAMA);

            return tamaViewModel;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao cadastrar TAMA");
            throw;
        }
    }

    //public Task<TamaViewModel> CadastrarTamaCompletoAsync(TamaViewModel tama, TamaInputCompletoViewModel complemento)
    //{
    //    throw new NotImplementedException();
    //}

    public TamaViewModel MapearParaViewModel(TamaInputCompletoViewModel input)
    {
        var recibo = input.ReciboRetencao ?? new ReciboRetencaoViewModel();

        var dataAssinaturaRecibo = recibo.DataAssinatura.HasValue && recibo.DataAssinatura.Value != default
            ? recibo.DataAssinatura
            : null;

        var dataAssinaturaInput = input.DataAssinatura.HasValue && input.DataAssinatura.Value != default
            ? input.DataAssinatura
            : null;

        var dataAssinaturaFinal = dataAssinaturaRecibo
            ?? dataAssinaturaInput
            ?? input.DataAplicacao;

        var viewModel = new TamaViewModel
        {
            NumeroTAMA = input.Id,
            versaoApp = input.VersaoApp,
            TermoInput = input.IdTipoInfracao,
            CepLocalRecolhimento = input.LocalCEP,
            EnderecoLocalRecolhimento = $"{input.LocalRua}, {input.LocalNumero} - {input.LocalBairro} {input.LocalComplemento}",
            MunicipioUfLocalRecolhimento = $"{input.LocalCidade}/{input.LocalEstado}",
            DataHoraLocalRecolhimento = input.AbordadoEm,
            BairroLocalInfracao = input.LocalBairro,
            NumeroLocalInfracao = input.LocalNumero,
            LatitudeLocalRecolhimento = string.Empty,
            LongitudeLocalRecolhimento = string.Empty,
            TransporteLocalRecolhimento = recibo.Opcoes?
                .FirstOrDefault(o => o.Tipo == "TransporteLocalRecolhimento")?.Valor?.ToString(),
            PlacaVeiculo = input.VeiculoPlaca,
            PaisVeiculo = input.VeiculoPais,
            MunicipioVeiculo = input.VeiculoEmplCidade,
            UfVeiculo = input.VeiculoEmplEstado,
            RenavamVeiculo = input.RenavamVeiculo ?? string.Empty,
            MarcaModeloVeiculo = input.VeiculoModelo,
            EspecieVeiculo = input.VeiculoEspecie,
            CategoriaVeiculo = input.VeiculoEspecie,
            CorVeiculo = input.VeiculoCor,
            Chassi = input.VeiculoChassi,
            VeiculoEmplacado = !string.IsNullOrEmpty(input.VeiculoPlaca) ? 'S' : 'N',
            NomeCondutor = recibo.CondutorNome ?? input.CondutorNome ?? string.Empty,
            CpfCondutor = recibo.CondutorCPF ?? input.condutorCPF ?? string.Empty,
            RgCondutor = string.Empty,
            CnhCondutor = recibo.CondutorCNH ?? input.CnhNumero ?? string.Empty,
            TelefoneCondutor = string.Empty,
            CepCondutor = input.LocalCEP,
            EnderecoCondutor = input.LocalRua,
            MunicipioUfCondutor = $"{input.LocalCidade}/{input.LocalEstado}",
            NumeroCondutor = input.LocalNumero,
            BairroCondutor = input.LocalBairro,
            NomeCondutorEntregue = recibo.CondutorNome ?? string.Empty,
            CpfCondutorEntregue = recibo.CondutorCPF ?? string.Empty,
            RgCondutorEntregue = string.Empty,
            CnhCondutorEntregue = recibo.CondutorCNH ?? string.Empty,
            TelefoneCondutorEntregue = string.Empty,
            CepCondutorEntregue = input.LocalCEP,
            EnderecoCondutorEntregue = input.LocalRua,
            MunicipioUfCondutorEntregue = $"{input.LocalCidade}/{input.LocalEstado}",
            MatriculaAgente = input.AplicadoPorMatricula,
            UsuarioInclusao = input.AplicadoPor,
            DataHoraInclusao = input.DataAplicacao,
            DataHoraAssinou = dataAssinaturaFinal,
            MatriculaAssinou = !string.IsNullOrEmpty(recibo.MatriculaAgenteAssinante)
            ? recibo.MatriculaAgenteAssinante
            : input.AplicadoPorMatricula,
            MatriculaCancelou = string.Empty,
            MatriculaTestemunha1 = string.Empty,
            MatriculaTestemunha2 = string.Empty,
            EstadoGeralLatariaPintura = recibo.Opcoes?
                .FirstOrDefault(o => o.Tipo == "EstadoGeralLatariaPintura")?.Nome ?? string.Empty,
            EquipamentosObrigatoriosAusentes = recibo.AusenciaEquipamentoObrigatorio,
            ObjetosEncontradosVeiculo = recibo.DescricaoObjetosEncontrados ?? string.Empty,
            VeiculoEntregueComChave = recibo.RemovidoComChave,
            NumeroReciboRetencaoTalonario = recibo.AIT ?? string.Empty,
            Observacoes = input.Observacoes,
            ObservacoesAdicionais = recibo.Observations ?? string.Empty,

            CodigoVerificador = string.Empty,

            AutosInfracao = new List<TermoAdocaoMedidaAdministrativa_AutosInfracaoViewModel>
        {
            new TermoAdocaoMedidaAdministrativa_AutosInfracaoViewModel
            {
                Numero = input.Id,
                Tipo = input.IdTipoInfracao
            }
        },

            DocumentosRecolhidos = recibo.Opcoes?
                .Where(o => o.Tipo == "DocumentosPossiveis")
                .Select(o => new TermoAdocaoMedidaAdministrativa_DocumentosRecolhidosViewModel
                {
                    Documento = o.Nome,
                    Numero = o.Valor?.ToString() ?? string.Empty
                }).ToList() ?? new List<TermoAdocaoMedidaAdministrativa_DocumentosRecolhidosViewModel>(),

            EquipamentosAusentes = recibo.AusenciaEquipamentoObrigatorio ?
                new List<TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentesViewModel>
                {
                new TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentesViewModel
                {
                    EquipamentoObrigatorio = "Equipamentos obrigatórios ausentes"
                }
                } : null
        };

        _logger.LogDebug("Dados mapeados - CPF: {CPF}, Transporte: {Transporte}",
            viewModel.CpfCondutor,
            viewModel.TransporteLocalRecolhimento);

        return viewModel;
    }
}