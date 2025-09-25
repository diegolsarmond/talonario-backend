using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Services
{
    public class CancelamentoAITService : ICancelamentoAITService
    {
        private readonly ICancelamentoAITRepository _repository;
        private readonly ILogger<CancelamentoAITService> _logger;

        public CancelamentoAITService(
            ICancelamentoAITRepository repository,
            ILogger<CancelamentoAITService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<int> RegistrarSolicitacaoAsync(SolicitacaoCancelamentoAITViewModel viewModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(viewModel.NumeroAutoInfracao))
                {
                    throw new ArgumentException("Número do auto de infração é obrigatório");
                }

                var entity = new SolicitacaoCancelamentoAITEntity
                {
                    NumeroAutoInfracao = viewModel.NumeroAutoInfracao,
                    Placa = viewModel.Placa,
                    Chassi = viewModel.Chassi,
                    MotivoCancelamento = viewModel.MotivoCancelamento,
                    CPFAgente = viewModel.CPFAgente,
                    SituacaoCancelamento = viewModel.SituacaoCancelamento
                };

                return await _repository.InserirAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar solicitação de cancelamento");
                throw;
            }
        }
    }
}