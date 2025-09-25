using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.ViewModels;
using static Dapper.SqlMapper;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public VeiculoRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Private Methods

        private static object CriaParametrosConsultaPorPlacaOuRenavam(string Placa, string Renavam)
        {
            if (!string.IsNullOrEmpty(Renavam) && !string.IsNullOrEmpty(Placa))
            {
                return new
                {
                    In_Placa = Placa,
                    In_Renavam = Renavam
                };
            }

            throw new ArgumentNullException("Placa e Renavam são obrigatórios");
        }

        #endregion Private Methods

        public async Task<VeiculoEntity> ConsultaExternaPorPlacaERenavam(string Placa, string Renavam)
        {
            object param = CriaParametrosConsultaPorPlacaOuRenavam(Placa, Renavam);

            try
            {
                var result = await _connection.QueryFirstOrDefaultAsync<VeiculoConsultaExternaEntity>("STP_RENAVAM_CONSULTA_HAMMER_API"
                                                                                        , param
                                                                                        , commandType: CommandType.StoredProcedure);
                return new VeiculoEntity
                {
                    Categoria = result.Categoria,
                    Chassi = result.Chassi,
                    Cor = result.Cor,
                    EstadoEmplacamento = result.UF,
                    MarcaModelo = result.Marca,
                    MunicipioEmplacamento = result.Municipio,
                    PaisDoVeiculo = result.Pais,
                    Placa = result.Placa,
                    Especie = result.Especie,
                    TipoVeiculo = result.TipoVeiculo
                };
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProprietarioEntity> ConsultaProprietarioPorPlaca(string placa)
        {
            string sql = $@"
                SELECT gp.Nome,
	                gp.DocPrincipal AS CPF,
	                gp.DataNascimento,
	                gp.Sexo,
	                COALESCE(rp.NomeMae, gp.NomeMae) AS NomeMae,
	                COALESCE(rp.NomePai, gp.NomePai) AS NomePai,
	                COALESCE(rh.NumeroRegistro, cc.NumeroRegistro) AS NumeroRegistro,
	                COALESCE(rc.CategoriaCNH, cc.CategoriaCNH) AS CategoriaCNH,
	                rhec.DataValidadeCNH,
	                rhec.UFHabilitacao
                FROM Rev_Veiculo rv
                INNER JOIN Gen_Pessoas gp ON rv.IdPessoaProprietario = gp.IdPessoa
                LEFT JOIN Ren_Pessoa rp ON gp.DocPrincipal = rp.CPF
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
                WHERE rv.Placa = @placa";

            var param = new { placa };
            var proprietario = await _connection.QuerySingleOrDefaultAsync<ProprietarioEntity>(sql, param);
            return proprietario;
        }

        public async Task<VeiculoEntity> ConsultaVeiculoPorChassi(string chassi)
        {
            string sql = $@"
                select top 1
	                dv.Placa,
	                dv.Marca as MarcaModelo,
	                rtrim(dv.Chassi) as Chassi,
	                dv.Categoria,
					dv.Especie,
					dv.TipoVeiculo,
	                dv.Cor,
	                dv.UFPlaca as EstadoEmplacamento,
	                dv.Cidade as MunicipioEmplacamento,
	                case
		                when dv.Nacionalidade = 'Nacional' then 'BRASIL'
		                else dv.Nacionalidade
	                end PaisDoVeiculo,
	                coalesce((select 1 from Fur_Ocorrencia oc
			                    where oc.Chassi = @chassi and Cancelamento_data is null)
                            , 0) FurtadoOuRoubado,
	                (select count(*) as Total
		                from Inf_AutoInfracao ai
		                where ai.SituacaoAuto not in (3,5)
		                and ai.placa = dv.Placa) TotalAutuacoes,
	                (select count(*) as Total
		                from Inf_AutoInfracao ai
		                where ai.SituacaoAuto in (3,5)
		                and ai.placa = dv.Placa) TotalMultas
                from
	                vw_Rev_DadosVeiculo dv
                where
                    Chassi = @chassi
                AND MotivoBaixa <> 2";

            var result = await _connection.QuerySingleOrDefaultAsync<VeiculoEntity>(sql, new { chassi });

            return result;
        }

        public async Task<VeiculoEntity> ConsultaVeiculoPorPlaca(string placa)
        {
            string sql = $@"
				SELECT TOP 1 dv.Placa,
					dv.Marca AS MarcaModelo,
					RTRIM(dv.Chassi) AS Chassi,
					dv.Categoria,
					dv.Especie,
					dv.TipoVeiculo,
					dv.Cor,
					dv.UFPlaca AS EstadoEmplacamento,
					dv.Cidade AS MunicipioEmplacamento,
					CASE
						WHEN dv.Nacionalidade = 'Nacional'
							THEN 'BRASIL'
						ELSE dv.Nacionalidade
						END AS PaisDoVeiculo,
					COALESCE((
							SELECT TOP 1 1
							FROM Fur_Ocorrencia oc
							WHERE oc.Placa = @placa
								AND oc.Cancelamento_data IS NULL
								AND Ocorrencia_Status = 1
							), 0) AS FurtadoOuRoubado,
					(
						SELECT COUNT(*) AS Total
						FROM Inf_AutoInfracao ai
						WHERE ai.SituacaoAuto NOT IN (
								3,
								5
								)
							AND ai.Placa = @placa
						) AS TotalAutuacoes,
					(
						SELECT COUNT(*) AS Total
						FROM Inf_AutoInfracao ai
						WHERE ai.SituacaoAuto IN (
								3,
								5
								)
							AND ai.Placa = @placa
						) AS TotalMultas
				FROM vw_Rev_DadosVeiculo dv
				WHERE Placa = @placa
                AND MotivoBaixa <> 2";

            var result = await _connection.QuerySingleOrDefaultAsync<VeiculoEntity>(sql, new { placa });

            return result;
        }

        public async Task<VeiculoEntity> ConsultaVeiculoPorRenavam(string renavam)
        {
            string sql = $@"
                select top 1
	                dv.Placa,
	                dv.Marca as MarcaModelo,
	                rtrim(dv.Chassi) as Chassi,
	                dv.Categoria,
					dv.Especie,
					dv.TipoVeiculo,
	                dv.Cor,
	                dv.UFPlaca as EstadoEmplacamento,
	                dv.Cidade as MunicipioEmplacamento,
	                case
		                when dv.Nacionalidade = 'Nacional' then 'BRASIL'
		                else dv.Nacionalidade
	                end PaisDoVeiculo,
	                coalesce((select 1 from Fur_Ocorrencia oc
			                    where oc.Renavam = @renavam and Cancelamento_data is null)
                            , 0) FurtadoOuRoubado,
	                (select count(*) as Total
		                from Inf_AutoInfracao ai
		                where ai.SituacaoAuto not in (3,5)
		                and ai.placa = dv.Placa) TotalAutuacoes,
	                (select count(*) as Total
		                from Inf_AutoInfracao ai
		                where ai.SituacaoAuto in (3,5)
		                and ai.placa = dv.Placa) TotalMultas
                from
	                vw_Rev_DadosVeiculo dv
                where
                    Renavam = @renavam";

            var result = await _connection.QuerySingleOrDefaultAsync<VeiculoEntity>(sql, new { renavam });

            return result;
        }

        public async Task<int> InserirVeiculoAbordado(VeiculoAbordadoViewModel veiculoAbordado)
        {
            string sql = $@"
            INSERT INTO Inf_VeiculoAbordado (Placa, JSON)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @Placa, @JSON
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _connection.QuerySingleAsync<int>(sql, new
            {
                Placa = veiculoAbordado.Placa,
                JSON = veiculoAbordado.JSON
            });

            return id;
        }

        public async Task<IEnumerable<CidadePaisEntity>> ObterCidades()
        {
            string sql = $@"
            SELECT
                c.idCidadePais,
                c.CodLocal,
                c.TipoLocal,
                c.NomeLocal,
                c.UF
            FROM Gen_CidadePais as c WITH (NOLOCK)
            WHERE TipoLocal = 'C'
            ORDER BY c.NomeLocal";

            var result = await _connection.QueryAsync<CidadePaisEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<IEnumerable<CorEntity>> ObterCores()
        {
            string sql = $@"
            SELECT
                c.idCor,
                c.Descricao,
                c.DataInclusao
            FROM Rev_Cor as c WITH (NOLOCK)
            ORDER BY c.Descricao";

            var result = await _connection.QueryAsync<CorEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<IEnumerable<EspecieEntity>> ObterEspecies()
        {
            string sql = $@"
            SELECT
                e.IdEspecie,
                e.Descricao,
                e.DescricaoAbreviada,
                e.DataInclusao
            FROM Rev_Especie as e WITH (NOLOCK)
            ORDER BY e.Descricao";

            var result = await _connection.QueryAsync<EspecieEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<IEnumerable<MarcaEntity>> ObterMarcas(
            int? page = 0, int? limit = 0)
        {
            string sql = $@"
            SELECT
                m.IdMarca,
                m.Descricao,
                m.idEspecie,
                m.DataInclusao,
                ROW_NUMBER() OVER (ORDER BY m.Descricao) AS RowNumber
            FROM Rev_Marca as m WITH (NOLOCK)";

            sql = $@"SELECT * FROM ({sql}) QResult";

            if (limit > 0)
                sql += $@" WHERE QResult.RowNumber BETWEEN @Offset AND @Limit";

            var result = await _connection.QueryAsync<MarcaEntity>(sql, new
            {
                Offset = (page <= 1) ? 0 : ((page * limit) - limit) + 1,
                Limit = page * limit
            });

            return result.ToList();
        }

        public async Task<IEnumerable<MarcaModeloTipoEntity>> ObterMarcasModelosTiposEspecies(
            int? page = 0, int? limit = 0)
        {
            string sql = $@"
            SELECT
                m.IdMarca,
                e.IdEspecie,
                m.Descricao as 'MarcaModelo',
                e.Descricao as 'Especie',
                t.Descricao as 'TipoVeiculo',
                t.RestricaoFazendaria,
                t.Porte,
                t.TemPlacaDianteira,
                t.PlacaPequena,
                t.PodeSerTaxi,
                t.PodeSerAmbulancia,
                t.PodeSerEscolar,
                m.DataInclusao,
                ROW_NUMBER() OVER (ORDER BY m.Descricao) AS RowNumber
            FROM Rev_Marca as m WITH (NOLOCK)
            INNER JOIN Rev_TipoVeiculo t ON m.idTipoVeiculo = t.IdTipoVeiculo
            INNER JOIN Rev_Especie e ON m.idEspecie = e.IdEspecie";

            sql = $@"SELECT * FROM ({sql}) QResult";

            if (limit > 0)
                sql += $@" WHERE QResult.RowNumber BETWEEN @Offset AND @Limit";

            var result = await _connection.QueryAsync<MarcaModeloTipoEntity>(sql, new
            {
                Offset = (page <= 1) ? 0 : ((page * limit) - limit) + 1,
                Limit = page * limit
            });

            return result.ToList();
        }

        public async Task<IEnumerable<CidadePaisEntity>> ObterPaises()
        {
            string sql = $@"
            SELECT
                c.idCidadePais,
                c.TipoLocal,
                c.NomeLocal,
                c.UF
            FROM Gen_CidadePais as c WITH (NOLOCK)
            WHERE TipoLocal = 'P'
            ORDER BY c.NomeLocal";

            var result = await _connection.QueryAsync<CidadePaisEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<VeiculoAbordadoEntity> ObterVeiculoAbordadoPorPlaca(string placa)
        {
            string sql = $@"
            SELECT
                Id,
                Placa,
                JSON
            FROM Inf_VeiculoAbordado
            WHERE Placa = @Placa";

            var result = await _connection.QuerySingleOrDefaultAsync<VeiculoAbordadoEntity>(sql, new
            {
                Placa = placa,
            });

            return result;
        }

        public async Task<IEnumerable<VeiculoAbordadoEntity>> ObterVeiculosAbordados()
        {
            string sql = $@"
            SELECT
                Id,
                Placa,
                JSON
            FROM Inf_VeiculoAbordado
            ORDER BY Id DESC";

            var result = await _connection.QueryAsync<VeiculoAbordadoEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<bool> RemoverVeiculoAbordadoPorPlaca(string placa)
        {
            string sql = $@"
            DELETE FROM Inf_VeiculoAbordado
            WHERE Placa = @Placa";

            var result = await _connection.ExecuteAsync(sql, new
            {
                Placa = placa,
            });

            return result > 0;
        }

        public async Task<int> TotalDeRegistros_Marcas()
        {
            string sql = $@"
            SELECT
                COUNT(1) as 'TotalDeRegistros'
            FROM Rev_Marca as m";

            var result = await _connection.QueryFirstAsync<int>(sql, new { });

            return result;
        }

        public async Task<int> TotalDeRegistros_MarcasModelosTiposEspecies()
        {
            string sql = $@"
            SELECT
                COUNT(1) as 'TotalDeRegistros'
            FROM Rev_Marca as m
            INNER JOIN Rev_TipoVeiculo t ON m.idTipoVeiculo = t.IdTipoVeiculo
            INNER JOIN Rev_Especie e ON m.idEspecie = e.IdEspecie";

            var result = await _connection.QueryFirstAsync<int>(sql, new { });

            return result;
        }
    }
}