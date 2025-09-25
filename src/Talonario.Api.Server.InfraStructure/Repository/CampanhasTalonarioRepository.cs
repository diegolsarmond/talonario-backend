using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using static Dapper.SqlMapper;

namespace Data.Repository.Talonario
{
    public class CampanhasTalonarioRepository : ICampanhasTalonarioRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection db;

        #endregion Private Fields

        #region Public Constructors

        public CampanhasTalonarioRepository()
        {
            _config = ConfigHelper.Load();
            db = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> Adicionar(InfCampanhasTalonario campanha)
        {
            try
            {
                string insertSQL = @"
            INSERT INTO dbo.Inf_CampanhasTalonario
            (Titulo, Descricao, DataInicio, DataFim, UsuarioInclusao, DataHoraInclusao, Ativo)
            VALUES
            (@Titulo, @Descricao, @DataInicio, @DataFim, @UsuarioInclusao, GETDATE(), @Ativo);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var parametros = new
                {
                    campanha.Titulo,
                    campanha.Descricao,
                    campanha.DataInicio,
                    campanha.DataFim,
                    campanha.UsuarioInclusao,
                    campanha.Ativo
                };

                int idGerado = await db.ExecuteScalarAsync<int>(insertSQL, parametros, commandType: CommandType.Text);
                return idGerado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar campanha", ex);
            }
        }

        public async Task<IEnumerable<InfCampanhasTalonario>> ObterAtivo()
        {
            try
            {
                string consultaSQL = @"
                    SELECT
                        Id,
                        Titulo,
                        Descricao,
                        DataInicio,
                        DataFim,
                        UsuarioInclusao,
                        UsuarioAlteracao,
                        UsuarioInativacao,
                        DataHoraInclusao,
                        DataHoraAlteracao,
                        DataHoraInativacao,
                        Ativo
                    FROM
                        dbo.Inf_CampanhasTalonario (NOLOCK)
                    WHERE
                        Ativo = 1";

                var result = await db.QueryAsync<InfCampanhasTalonario>(consultaSQL, commandType: CommandType.Text);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter campanhas ativas", ex);
            }
        }

        public async Task<IEnumerable<InfCampanhasTalonario>> ObterInativas()
        {
            try
            {
                string consultaSQL = @"
                    SELECT
                        Id,
                        Titulo,
                        Descricao,
                        DataInicio,
                        DataFim,
                        UsuarioInclusao,
                        UsuarioAlteracao,
                        UsuarioInativacao,
                        DataHoraInclusao,
                        DataHoraAlteracao,
                        DataHoraInativacao,
                        Ativo
                    FROM
                        dbo.Inf_CampanhasTalonario (NOLOCK)
                    WHERE
                        Ativo = 0 OR Ativo IS NULL";

                var result = await db.QueryAsync<InfCampanhasTalonario>(consultaSQL, commandType: CommandType.Text);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter campanhas inativas", ex);
            }
        }

        #endregion Public Methods
    }
}