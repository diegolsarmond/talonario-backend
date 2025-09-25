using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class CondutorRepository : ICondutorRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public CondutorRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CondutorEntity> PesquisarExternaPorCPF(string cpf)
        {
            try
            {
                string sql = @"EXEC stp_Ren_ConsultaBincoT575_Sel @CPF = @DocPrincipal, @IdSessao = -1";
                Gen_ConsultaBinco retornoSP = await _connection.QueryFirstOrDefaultAsync<Gen_ConsultaBinco>(sql, new { DocPrincipal = cpf });

                if (retornoSP is null)
                    return null;

                return new CondutorEntity
                {
                    CategoriaCNH = retornoSP.outCategoriaAtual,
                    CPF = retornoSP.outCPF,
                    DataNascimento = retornoSP.outDataNascimento,
                    DataValidadeCNH = retornoSP.outDataValidade,
                    Nome = retornoSP.outNomeCondutor,
                    NomeMae = retornoSP.outNomeMae,
                    NomePai = retornoSP.outNomePai,
                    NumeroRegistro = retornoSP.outNumeroRegistro,
                    Sexo = retornoSP.outSexo,
                    UFHabilitacao = retornoSP.outPrimeiraHabilitacaoUF
                };
            }
            catch (SqlException)
            {
                //Esse catch existe pois a store procedure que faz a pesquisa externa não funciona em ambiente de desenvolvimento
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CondutorEntity> PesquisarPorCpf(string cpf)
        {
            string sql = $@"
                SELECT rp.Nome,
	                rp.CPF,
	                rp.DataNascimento,
	                rp.Sexo,
	                rp.NomeMae,
	                rp.NomePai,
	                COALESCE(rh.NumeroRegistro, cc.NumeroRegistro) AS NumeroRegistro,
	                COALESCE(rc.CategoriaCNH, cc.CategoriaCNH) AS CategoriaCNH,
	                rhec.DataValidadeCNH,
	                rhec.UFHabilitacao
                FROM Ren_Pessoa rp
                LEFT JOIN Carga_CNH cc ON rp.IdPessoa = cc.CodigoIdentificadorPessoa
                LEFT JOIN Ren_Habilitacao rh ON rh.IdPessoa = rp.IdPessoa
	                AND rh.UFDominio = 'RO'
	                AND rh.Excluida = 0
                LEFT JOIN Ren_Categoria rc ON rc.idCategoria = rh.idCategoria
                LEFT JOIN (
	                SELECT IdPessoa,
		                MAX(DataValidadeCNH) AS DataValidadeCNH,
		                MAX(UFHabilitacao) AS UFHabilitacao
	                FROM Ren_HistoricoExpedicaoCNH
	                GROUP BY IdPessoa
	                ) rhec ON rp.IdPessoa = rhec.IdPessoa
                WHERE rp.CPF = @cpf";

            var param = new { cpf };
            var result = await _connection.QuerySingleOrDefaultAsync<CondutorEntity>(sql, param);

            return result;
        }

        #endregion Public Methods
    }
}