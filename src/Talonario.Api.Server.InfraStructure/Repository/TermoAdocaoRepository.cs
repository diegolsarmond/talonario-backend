using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class TermoAdocaoRepository : ITermoAdocaoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TermoAdocaoRepository> _logger;

        public TermoAdocaoRepository(IConfiguration configuration, ILogger<TermoAdocaoRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("AtelierDataBase");
            _logger = logger;
        }

        public async Task<IEnumerable<string>> ObterValoresDistintosEstadoLatariaAsync()
        {
            const string sql = @"
            SELECT DISTINCT EstadoGeralLatariaPintura
            FROM Inf_TermoAdocaoMedidaAdministrativa
            WHERE EstadoGeralLatariaPintura IS NOT NULL AND EstadoGeralLatariaPintura != ''";

            try
            {
                using var db = new SqlConnection(_connectionString);
                return await db.QueryAsync<string>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter valores distintos de EstadoGeralLatariaPintura");
                throw;
            }
        }

        public async Task<IEnumerable<string>> ObterValoresDistintosTransporteAsync()
        {
            const string sql = @"
            	        SELECT DISTINCT TransporteLocalRecolhimento
        FROM Inf_TermoAdocaoMedidaAdministrativa
        WHERE TransporteLocalRecolhimento IS NOT NULL
          AND TransporteLocalRecolhimento != ''
          AND TransporteLocalRecolhimento != 'caminhão'";

            try
            {
                using var db = new SqlConnection(_connectionString);
                return await db.QueryAsync<string>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter valores distintos de TransporteLocalRecolhimento");
                throw;
            }
        }

        public async Task<IEnumerable<Application.Entities.EquipamentoObrigatorioEntity>> ObterEquipamentosObrigatoriosAsync()
        {
            const string sql = @"
                SELECT IdEquipamentoObrigatorio, Titulo, Descricao
                FROM Gen_EquipamentoObrigatorio";

            try
            {
                using var db = new SqlConnection(_connectionString);
                return await db.QueryAsync<EquipamentoObrigatorioEntity>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter equipamentos obrigatórios");
                throw;
            }
        }

        public async Task<IEnumerable<DocumentoViewModel>> ObterDocumentosPossiveisAsync()
        {
            const string query = "SELECT IdDocumentoRecolhido, Titulo FROM Gen_DocumentoRecolhido ORDER BY IdDocumentoRecolhido";
            using var db = new SqlConnection(_connectionString);
            return await db.QueryAsync<DocumentoViewModel>(query);
        }

        //public Task<IEnumerable<DocumentoViewModel>> ObterDocumentosRecolhidosAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}