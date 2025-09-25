using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;

namespace Talonario.Api.Server.Infrastructure.Repositories
{
    public class CancelamentoAITRepository : ICancelamentoAITRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CancelamentoAITRepository> _logger;

        public CancelamentoAITRepository(
            IConfiguration configuration,
            ILogger<CancelamentoAITRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("AtelierDataBase");
            _logger = logger;
        }

        public async Task<int?> ObterIdAutoInfracaoAsync(string numeroAutoInfracao)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"
                SELECT ai.IdAutoInfracao
                FROM Inf_RegistroLote rl
                INNER JOIN Inf_TipoInfracao ti
                    ON ti.CodigoInfracao = rl.CodigoInfracao
                   AND ti.Desdobramento = rl.Desdobramento
                INNER JOIN Inf_AutoInfracao ai
                    ON rl.IdRegistroLote = ai.IdRegistroLote
                WHERE rl.NumeroAutoInfracao = @NumeroAutoInfracao;";

            return await db.QueryFirstOrDefaultAsync<int?>(sql, new { NumeroAutoInfracao = numeroAutoInfracao });
        }

        public async Task<int> InserirAsync(SolicitacaoCancelamentoAITEntity entity)
        {
            using var db = new SqlConnection(_connectionString);

            try
            {
                entity.IdAutoInfracao = await ObterIdAutoInfracaoAsync(entity.NumeroAutoInfracao)
                    ?? throw new ApplicationException("Auto de infração não encontrado.");

                var sql = @"
                    INSERT INTO Inf_SolicitacaoCancelamentoAIT (
                        NumeroAutoInfracao,
                        Placa,
                        Chassi,
                        MotivoCancelamento,
                        CPFAgente,
                        SituacaoCancelamento,
                        ObservacaoCancelamento,
                        DataCancelamento,
                        UsuarioCancelamento,
                        DataHoraSolicitacao,
                        IdAutoInfracao
                    )
                    VALUES (
                        @NumeroAutoInfracao,
                        @Placa,
                        @Chassi,
                        @MotivoCancelamento,
                        @CPFAgente,
                        @SituacaoCancelamento,
                        @ObservacaoCancelamento,
                        @DataCancelamento,
                        @UsuarioCancelamento,
                        GETDATE(),
                        @IdAutoInfracao
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                _logger.LogInformation("Executando inserção de cancelamento AIT");

                return await db.QuerySingleAsync<int>(sql, entity);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                _logger.LogError(ex, "Erro de duplicidade ao inserir cancelamento AIT");
                throw new ApplicationException("Já existe uma solicitação com esses dados", ex);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro SQL ao inserir cancelamento AIT. Número do erro: {ErrorNumber}", ex.Number);
                throw new ApplicationException($"Erro de banco de dados: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao inserir cancelamento AIT");
                throw;
            }
        }
    }
}