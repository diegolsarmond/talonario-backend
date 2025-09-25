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
    public class UsuarioRepository : IUsuarioRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;
        private static SqlConnection _connectionAtelier;

        #endregion Private Fields

        #region Public Constructors

        public UsuarioRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AutenticadorDataBase"));
            _connectionAtelier = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> AtualizaUsuarioLogado(string cpf, string idDispositivo)
        {
            string sql = $@"
            DELETE FROM [Inf_TalonarioUsuarioLogado]
            WHERE [idDispositivo] = @IdDispositivo
            AND [cpf] <> @CPF;

            UPDATE [dbo].[Inf_TalonarioUsuarioLogado]
               SET [idDispositivo] = @IdDispositivo
                  ,[dataAutenticacao] = GETDATE()
             WHERE [cpf] = @CPF;";

            var result = await _connectionAtelier.ExecuteAsync(sql, new
            {
                CPF = cpf,
                IdDispositivo = idDispositivo
            });

            return result > 0;
        }

        public async Task<int> InsereUsuarioLogado(string cpf, string idDispositivo)
        {
            string sql = $@"
            DELETE FROM [Inf_TalonarioUsuarioLogado]
            WHERE [idDispositivo] = @IdDispositivo
            AND [cpf] <> @CPF;

            INSERT INTO [Inf_TalonarioUsuarioLogado]
                        ([cpf], [idDispositivo], [dataAutenticacao])
                 OUTPUT INSERTED.Id
                 VALUES (@CPF, @IdDispositivo, GETDATE());";

            return await _connectionAtelier.QuerySingleAsync<int>(sql, new
            {
                CPF = cpf,
                IdDispositivo = idDispositivo
            });
        }

        public async Task<bool> Logout(string cpf, string idDispositivo)
        {
            string sql = @"
                            DECLARE @DadosDeletados
                            TABLE (cpf varchar(50),
	                                idDispositivo varchar(100),
	                                dataAutenticacao datetime);

                            DELETE FROM
	                            Inf_TalonarioUsuarioLogado
                            OUTPUT
	                            DELETED.cpf,
	                            DELETED.idDispositivo,
	                            DELETED.dataAutenticacao
                            INTO
	                            @DadosDeletados
                            WHERE
	                            cpf = @cpf
	                            and idDispositivo = @idDispositivo;

                            INSERT INTO
	                            Inf_TalonarioUsuarioLogadoHistorico
	                            (cpf, idDispositivo, dataAutenticacao, dataLogout)
                            OUTPUT INSERTED.Id
                            SELECT
	                                cpf, idDispositivo, dataAutenticacao, GetDate()
                            FROM @DadosDeletados;";

            var param = new { cpf, idDispositivo };

            await _connectionAtelier.OpenAsync();
            using SqlTransaction sqlTransaction = _connectionAtelier.BeginTransaction();

            try
            {
                int idRegistroInserido = (await _connectionAtelier.QueryAsync<int>(sql, param, sqlTransaction)).First();

                if (idRegistroInserido == 0)
                {
                    sqlTransaction.Rollback();
                    return false;
                }

                sqlTransaction.Commit();
                return true;
            }
            catch
            {
                sqlTransaction.Rollback();
                return false;
            }
            finally
            {
                await _connectionAtelier.CloseAsync();
            }
        }

        public async Task<IEnumerable<UsuarioEntity>> ObterPorCPF(string cpf)
        {
            string sql = $@"
            SELECT
                u.Id,
                u.Nome,
                u.Usuario,
                u.CPF,
                u.Email,
                u.Senha,
                u.Ativo,
                IIF(a.Descricao = 'Assinatura', 'Assinatura', 'Normais') as Permissoes,
	            LEFT(e.Descricao, CHARINDEX(' - ', e.Descricao) - 1) as IdEmpresa,
                e.Descricao as Empresa
            FROM usuarios u
                inner join usuario_perfil up on u.Id = up.usuarioId
                inner join usuario_empresa ue on ue.usuarioId = up.usuarioId
                inner join empresas e on e.Id = ue.empresaId
                inner join perfis_itens pis on up.perfilId  = pis.IdPerfil
                inner join acoes a on pis.IdAcao = a.Id
                inner join sistemas s on pis.IdSistema = s.Id
            WHERE
                s.Descricao LIKE '%Talonario%'
                and u.CPF = @CPF";

            var result = await _connection.QueryAsync<UsuarioEntity>(sql, new
            {
                CPF = cpf
            });

            return result.ToList();
        }

        public async Task<IEnumerable<UsuarioEntity>> ObterTodos()
        {
            string sql = $@"
                SELECT u.Id,
                    u.Nome,
                    u.Usuario,
                    u.CPF,
                    u.Email,
                    u.Senha,
                    u.Ativo,
                    IIF(a.Descricao = 'Assinatura', 'Assinatura', 'Normais') AS Permissoes,
                    IIF(CHARINDEX(' - ', e.Descricao) != 0, LEFT(e.Descricao, CHARINDEX(' - ', e.Descricao) - 1), '0') AS IdEmpresa,
                    e.Descricao AS Empresa,
                    u.Matricula,
	                0 AS MatriculaAgente
                FROM [usuarios] u
                INNER JOIN [usuario_perfil] up ON u.Id = up.usuarioId
                INNER JOIN [usuario_empresa] ue ON ue.usuarioId = up.usuarioId
                INNER JOIN [empresas] e ON e.Id = ue.empresaId
                INNER JOIN [perfis_itens] pis ON up.perfilId = pis.IdPerfil
                INNER JOIN [acoes] a ON pis.IdAcao = a.Id
                INNER JOIN [sistemas] s ON pis.IdSistema = s.Id
                WHERE s.Descricao LIKE '%Talonario%'";

            var result = await _connection.QueryAsync<UsuarioEntity>(sql, new
            {
            });

            return result.ToList();
        }

        public async Task<bool> PodeAssinar(string matricula)
        {
            string sql = @"	select 1
	                        from Inf_AgenteAutuador
	                        where
	                        (Ativo = 1
                            OR (DataInicioOperacao <= GetDate() AND DataFimOperacao >= GetDate()))
	                        and MatriculaAgente = @matricula";

            var param = new { matricula };

            var retorno = await _connectionAtelier.QueryFirstOrDefaultAsync<int?>(sql, param);
            return retorno.HasValue;
        }

        public int RetornaMatriculaAgente(int matricula)
        {
            try
            {
                string sql = $@"
                SELECT TOP 1 MatriculaAgente
                FROM [Inf_AgenteAutuador]
                WHERE MatriculaAgente = @matricula
                AND Ativo = 1";

                var param = new { matricula };

                var retorno = _connectionAtelier.QueryFirstOrDefault<int?>(sql, param);
                return retorno.Value;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<UsuarioLogadoEntity> VerificaUsuarioLogado(string cpf)
        {
            string sql = $@"
            SELECT [id]
                  ,[cpf]
                  ,[idDispositivo]
                  ,[dataAutenticacao]
              FROM [Inf_TalonarioUsuarioLogado]
             WHERE [cpf] = @CPF";

            return await _connectionAtelier.QuerySingleOrDefaultAsync<UsuarioLogadoEntity>(sql, new { CPF = cpf });
        }

        #endregion Public Methods
    }
}