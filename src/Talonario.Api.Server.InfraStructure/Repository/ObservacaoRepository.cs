using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class ObservacaoRepository : IObservacaoRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public ObservacaoRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<ObservacaoEntity>> GetAllAtivos()
        {
            string sql = $@"SELECT
	                            Id
                                ,Titulo
                                ,Descricao
                            FROM
	                            Inf_ObservacoesTalonario
                            WHERE
	                            Ativo = 1";

            return (await _connection.QueryAsync<ObservacaoEntity>(sql)).ToList();
        }

        #endregion Public Methods
    }
}