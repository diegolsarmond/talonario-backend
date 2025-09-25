using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using static Dapper.SqlMapper;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class OrgaoAutuadorRepository : IOrgaoAutuadorRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public OrgaoAutuadorRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<OrgaoAutuadorEntity>> ObterOrgaoAutuadorPorEmpresa(List<int> empresaCodigoOrgao)
        {
            string sql = $@"
            SELECT
                oa.CodigoOrgaoAutuador,
                oa.CNPJOrgaoAutuador,
                oa.NomeOrgaoAutuador,
                oa.SiglaOrgaoAutuador,
                oa.UFOrgaoAutuador,
                oa.TipoOrgaoAutuador,
                oa.CodigoConvenio,
                oa.DataInclusao
            FROM Inf_OrgaoAutuador oa
            WHERE oa.CodigoOrgaoAutuador IN @EmpresaCodigoOrgao
            ORDER BY oa.CodigoOrgaoAutuador DESC";

            var result = await _connection.QueryAsync<OrgaoAutuadorEntity>(sql, new
            {
                EmpresaCodigoOrgao = empresaCodigoOrgao.ToArray()
            });

            return result.ToList();
        }

        #endregion Public Methods
    }
}