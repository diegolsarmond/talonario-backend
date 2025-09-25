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
    public class PessoaRepository : IPessoaRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public PessoaRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> InserirPessoaAbordada(PessoaAbordadaEntity veiculoAbordado)
        {
            string sql = $@"
            INSERT INTO Inf_PessoaAbordada (IdPessoa, JSON)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @IdPessoa, @JSON
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _connection.QuerySingleAsync<int>(sql, new
            {
                IdPessoa = veiculoAbordado.IdPessoa,
                JSON = veiculoAbordado.JSON
            });

            return id;
        }

        public async Task<PessoaAbordadaEntity> ObterPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            string sql = $@"
            SELECT [Id]
                  ,[IdPessoa]
                  ,[JSON]
              FROM [Inf_PessoaAbordada]
             WHERE IdPessoa = @IdPessoa";

            var result = await _connection.QuerySingleOrDefaultAsync<PessoaAbordadaEntity>(sql, new { IdPessoa = idPessoa });

            return result;
        }

        public async Task<IEnumerable<PessoaAbordadaEntity>> ObterPessoasAbordadas()
        {
            string sql = $@"
            SELECT [Id]
                  ,[IdPessoa]
                  ,[JSON]
              FROM [Inf_PessoaAbordada]";

            var result = await _connection.QueryAsync<PessoaAbordadaEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<PessoaEntity> PesquisarExternaPorCPF(string cpf)
        {
            try
            {
                string sql = @"EXEC stp_Ren_ConsultaBincoT575_Sel @CPF = @DocPrincipal, @IdSessao = -1";
                Gen_ConsultaBinco retornoSP = await _connection.QueryFirstOrDefaultAsync<Gen_ConsultaBinco>(sql, new { DocPrincipal = cpf });

                if (retornoSP is null)
                    return null;

                return new PessoaEntity
                {
                    Id = 0,
                    CPF = retornoSP.outCPF,
                    DataNascimento = retornoSP.outDataNascimento,
                    Habilitacao = retornoSP.outNumeroCNH,
                    Nome = retornoSP.outNomeCondutor,
                    NomeMae = retornoSP.outNomeMae,
                    NomePai = retornoSP.outNomePai,
                    Renach = retornoSP.outNumeroRENACH,
                    Sexo = retornoSP.outSexo == "1" ? "Masculino" : "Feminino",
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

        public async Task<PessoaEntity> PesquisarPorCPF(string cpf)
        {
            try
            {
                string sql = $@"
SELECT top 1
     GP.IdPessoa Id
    ,GP.Nome
    ,GP.CPF
    ,GP.DataNascimento
	,CASE
		WHEN GP.Sexo = 1 THEN 'Masculino'
		WHEN GP.Sexo = 2 THEN 'Feminino'
		ELSE 'Não Cadastrado'
	END AS Sexo
	,GP.NomePai
	,GP.NomeMae
    ,RD.NumeroRegistro as Habilitacao
	,RD.UFHabilitacaoAtual UFHabilitacao
	,RD.NumeroRenach Renach
FROM
    Ren_Pessoa AS GP
    INNER JOIN Ren_Habilitacao AS RD
    ON GP.IDPESSOA = RD.IDPESSOA
WHERE
    GP.CPF = @cpf
AND RD.Excluida = 0";

                var param = new { cpf };

                return await _connection.QuerySingleOrDefaultAsync<PessoaEntity>(sql, param);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoverPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            string sql = $@"
            DELETE FROM Inf_PessoaAbordada
            WHERE IdPessoa = @IdPessoa";

            var result = await _connection.ExecuteAsync(sql, new { IdPessoa = idPessoa });

            return result > 0;
        }

        #endregion Public Methods
    }
}