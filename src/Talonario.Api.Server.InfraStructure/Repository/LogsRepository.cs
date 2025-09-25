using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;

namespace Talonario.Api.Server.Infrastructure.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<LogsRepository> _logger;

        public LogsRepository(
            IConfiguration configuration,
            ILogger<LogsRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("AtelierDataBase");
            _logger = logger;
        }

        public async Task InserirLoteAsync(List<RegistroLogTalonarioEntity> logs)
        {
            using var db = new SqlConnection(_connectionString);
            await db.OpenAsync();
            using var transaction = db.BeginTransaction();

            try
            {
                var sql = @"INSERT INTO Inf_RegistroLogsTalonario 
                            (NomeAgente, CpfAgente, Acao, DataHora, Dispositivo, Modulo, AppVersao, Falha)
                            VALUES (@NomeAgente, @CpfAgente, @Acao, @DataHora, @Dispositivo, @Modulo, @AppVersao, @Falha)";

                await db.ExecuteAsync(sql, logs, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                _logger.LogError(ex, "Erro ao registrar logs em lote");
                throw;
            }
        }
    }
}