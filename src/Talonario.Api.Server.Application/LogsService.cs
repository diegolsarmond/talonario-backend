using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Services
{
    public class LogsService : ILogsService
    {
        private readonly ILogsRepository _repository;
        private readonly ILogger<LogsService> _logger;

        public LogsService(ILogsRepository repository, ILogger<LogsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task RegistrarLogsAsync(List<RegistroLogTalonarioViewModel> logs)
        {
            if (logs == null || !logs.Any())
                throw new ArgumentException("Nenhum log informado.");

            foreach (var log in logs)
            {
                if (string.IsNullOrWhiteSpace(log.CpfAgente) ||
                    string.IsNullOrWhiteSpace(log.Acao) ||
                    string.IsNullOrWhiteSpace(log.Modulo) ||
                    log.DataHora == default)
                {
                    throw new ArgumentException("Campos obrigatórios não preenchidos.");
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(log.AppVersao, @"^\d+\.\d+\.\d+$"))
                    throw new ArgumentException($"Versão inválida: {log.AppVersao}");
            }

            var entities = logs.Select(l => new RegistroLogTalonarioEntity
            {
                NomeAgente = l.NomeAgente,
                CpfAgente = l.CpfAgente,
                Acao = l.Acao,
                DataHora = l.DataHora,
                Dispositivo = l.Dispositivo,
                Modulo = l.Modulo,
                AppVersao = l.AppVersao,
                Falha = l.Falha ?? false,
            }).ToList();

            await _repository.InserirLoteAsync(entities);
        }
    }
}