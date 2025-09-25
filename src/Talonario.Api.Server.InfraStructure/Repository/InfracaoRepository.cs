using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.ViewModels;
using static Dapper.SqlMapper;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class InfracaoRepository : IInfracaoRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public InfracaoRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> AtualizarInfracaoUsuarioDispositivo(string idTalonarioDispositivo, string sequencia)
        {
            string sql = $@"
            UPDATE Inf_TalonarioDispositivo
            SET Sequencia = @Sequencia
            WHERE Id = @Id
            AND CAST(Sequencia AS INT) < CAST(@Sequencia AS INT)";

            var result = await _connection.ExecuteAsync(sql, new
            {
                Id = idTalonarioDispositivo,
                Sequencia = sequencia
            });

            return result > 0;
        }

        public bool AtualizarInfracaoUsuarioDispositivo2(string idTalonarioDispositivo, string sequencia)
        {
            string sql = $@"
            UPDATE Inf_TalonarioDispositivo
            SET Sequencia = @Sequencia
            WHERE Id = @Id
            AND CAST(Sequencia AS INT) < CAST(@Sequencia AS INT)";

            var result = _connection.Execute(sql, new
            {
                Id = idTalonarioDispositivo,
                Sequencia = sequencia
            });

            return result > 0;
        }

        public async Task<bool> AtualizarMotivoProcessamento(string AIT, string MotivoProcessamento)
        {
            string sql = string.Empty;

            if (MotivoProcessamento == "SUCESSO")
            {
                sql = $@"
                    UPDATE Inf_InfracaoNaoTransmitida
                    SET DataEnviado = @DataEnviado
                    WHERE AIT = @AIT";

                var result = await _connection.ExecuteAsync(sql, new
                {
                    AIT = AIT,
                    DataEnviado = DateTime.Now
                });

                return result > 0;
            }
            else
            {
                sql = $@"
                    UPDATE Inf_InfracaoNaoTransmitida
                    SET MotivoProcessamento = @MotivoProcessamento
                    WHERE AIT = @AIT";

                var result = await _connection.ExecuteAsync(sql, new
                {
                    AIT = AIT,
                    MotivoProcessamento = MotivoProcessamento
                });

                return result > 0;
            }
        }

        public bool AtualizarMotivoProcessamento2(string AIT, string MotivoProcessamento)
        {
            string sql = string.Empty;

            if (MotivoProcessamento == "SUCESSO")
            {
                sql = $@"
                    UPDATE Inf_InfracaoNaoTransmitida
                    SET DataEnviado = @DataEnviado
                    WHERE AIT = @AIT";

                var result = _connection.Execute(sql, new
                {
                    AIT = AIT,
                    DataEnviado = DateTime.Now
                });

                return result > 0;
            }
            else
            {
                sql = $@"
                    UPDATE Inf_InfracaoNaoTransmitida
                    SET MotivoProcessamento = @MotivoProcessamento
                    WHERE AIT = @AIT";

                var result = _connection.Execute(sql, new
                {
                    AIT = AIT,
                    MotivoProcessamento = MotivoProcessamento
                });

                return result > 0;
            }
        }

        public async Task<int> InserirInfracaoAnexo(string ait, string anexoBase64)
        {
            string sql = $@"
            INSERT INTO Inf_InfracaoAnexo (AIT, AnexoBase64)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @AIT, @AnexoBase64
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _connection.QuerySingleAsync<int>(sql, new
            {
                AIT = ait,
                AnexoBase64 = anexoBase64
            });
        }

        public int InserirInfracaoAnexo2(string ait, string anexoBase64)
        {
            string sql = $@"
            INSERT INTO Inf_InfracaoAnexo (AIT, AnexoBase64)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @AIT, @AnexoBase64
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            return _connection.QuerySingle<int>(sql, new
            {
                AIT = ait,
                AnexoBase64 = anexoBase64
            });
        }

        public async Task<int> InserirInfracaoNaoTransmitida(InfracaoNaoTransmitidaViewModel infracaoNaoTransmitida)
        {
            string sql = $@"
            INSERT INTO Inf_InfracaoNaoTransmitida (AIT, JSON, Tipo, DataCancelamento, DataEnviado)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @AIT, @JSON, @Tipo, @DataCancelamento, @DataEnviado
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _connection.QuerySingleAsync<int>(sql, new
            {
                AIT = infracaoNaoTransmitida.AIT,
                JSON = infracaoNaoTransmitida.JSON,
                Tipo = infracaoNaoTransmitida.Tipo.Trim().ToLower(),
                DataCancelamento = infracaoNaoTransmitida.DataCancelamento,
                DataEnviado = infracaoNaoTransmitida.DataEnviado
            });

            return id;
        }

        public async Task<int> InserirInfracaoPdf(string ait, string pdfBase64)
        {
            string sql = $@"
            INSERT INTO Inf_AutoInfracaoPDF (ait, pdf)
            OUTPUT INSERTED.id --OUTPUT INSERTED.*
            VALUES (
                @ait, @pdfBase64
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _connection.QuerySingleAsync<int>(sql, new
            {
                ait = ait,
                pdfBase64 = pdfBase64
            });
        }

        public async Task<int> InserirInfracaoPessoa(InfracaoPessoaViewModel infracaoPessoa)
        {
            string sql = $@"
            INSERT INTO [Inf_InfracaoPessoa]
                       ([NumeroAuto]
                       ,[Nome]
                       ,[CPF]
                       ,[RG]
                       ,[NomeMae]
                       ,[NomePai]
                       ,[DataNascimento]
                       ,[Genero]
                       ,[PessoaEndCep]
                       ,[PessoaEndRua]
                       ,[PessoaEndBairro]
                       ,[PessoaEndNumero]
                       ,[PessoaEndComplemento]
                       ,[PessoaEndCidade]
                       ,[PessoaEndEstado]
                       ,[LocalId]
                       ,[LocalCodigoMunicipio]
                       ,[LocalCep]
                       ,[LocalMunicipio]
                       ,[LocalComplemento]
                       ,[LocalBairro]
                       ,[LocalNumero]
                       ,[LocalEstado]
                       ,[LocalLogradouro]
                       ,[DataAbordagem]
                       ,[NomeAgente]
                       ,[CpfAgente]
                       ,[AbordadoPeloOrgao]
                       ,[AbordadoPeloIdOrgao]
                       ,[AbordadoPeloOrgaoCompetencia]
                       ,[IdTipoInfracao]
                       ,[ArtigoCodigo]
                       ,[ArtigoDesdobramento]
                       ,[ArtigoCompetencia]
                       ,[ArtigoNatureza]
                       ,[Artigo]
                       ,[ArtigoInfrator]
                       ,[ArtigoDescricao]
                       ,[ArtigoDescricaoCompleta]
                       ,[ArtigoApreensaoVeiculo]
                       ,[ArtigoSuspenderCNH]
                       ,[ArtigoApresentaCondutor]
                       ,[ArtigoRemoverVeiculo]
                       ,[ArtigoConfiscatePlates]
                       ,[ArtigoCargaExcessiva]
                       ,[ArtigoRequerDocumento]
                       ,[ArtigoRequerEquipamento]
                       ,[ArtigoEmVigencia]
                       ,[ArtigoInicioVigencia]
                       ,[ArtigoFinalVigencia]
                       ,[ArtigoPontos]
                       ,[ArtigoEquipamento]
                       ,[ArtigoValorMulta])
                 OUTPUT INSERTED.Id --OUTPUT INSERTED.*
                 VALUES
                       (@NumeroAuto
                       ,@Nome
                       ,@CPF
                       ,@RG
                       ,@NomeMae
                       ,@NomePai
                       ,@DataNascimento
                       ,@Genero
                       ,@PessoaEndCep
                       ,@PessoaEndRua
                       ,@PessoaEndBairro
                       ,@PessoaEndNumero
                       ,@PessoaEndComplemento
                       ,@PessoaEndCidade
                       ,@PessoaEndEstado
                       ,@LocalId
                       ,@LocalCodigoMunicipio
                       ,@LocalCep
                       ,@LocalMunicipio
                       ,@LocalComplemento
                       ,@LocalBairro
                       ,@LocalNumero
                       ,@LocalEstado
                       ,@LocalLogradouro
                       ,@DataAbordagem
                       ,@NomeAgente
                       ,@CpfAgente
                       ,@AbordadoPeloOrgao
                       ,@AbordadoPeloIdOrgao
                       ,@AbordadoPeloOrgaoCompetencia
                       ,@IdTipoInfracao
                       ,@ArtigoCodigo
                       ,@ArtigoDesdobramento
                       ,@ArtigoCompetencia
                       ,@ArtigoNatureza
                       ,@Artigo
                       ,@ArtigoInfrator
                       ,@ArtigoDescricao
                       ,@ArtigoDescricaoCompleta
                       ,@ArtigoApreensaoVeiculo
                       ,@ArtigoSuspenderCNH
                       ,@ArtigoApresentaCondutor
                       ,@ArtigoRemoverVeiculo
                       ,@ArtigoConfiscatePlates
                       ,@ArtigoCargaExcessiva
                       ,@ArtigoRequerDocumento
                       ,@ArtigoRequerEquipamento
                       ,@ArtigoEmVigencia
                       ,@ArtigoInicioVigencia
                       ,@ArtigoFinalVigencia
                       ,@ArtigoPontos
                       ,@ArtigoEquipamento
                       ,@ArtigoValorMulta) --SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _connection.QuerySingleAsync<int>(sql, new
            {
                NumeroAuto = infracaoPessoa.NumeroAuto,
                Nome = infracaoPessoa.Nome,
                CPF = infracaoPessoa.CPF,
                RG = infracaoPessoa.RG,
                NomeMae = infracaoPessoa.NomeMae,
                NomePai = infracaoPessoa.NomePai,
                DataNascimento = infracaoPessoa.DataNascimento,
                Genero = infracaoPessoa.Genero,
                PessoaEndCep = infracaoPessoa.PessoaEndCep,
                PessoaEndRua = infracaoPessoa.PessoaEndRua,
                PessoaEndBairro = infracaoPessoa.PessoaEndBairro,
                PessoaEndNumero = infracaoPessoa.PessoaEndNumero,
                PessoaEndComplemento = infracaoPessoa.PessoaEndComplemento,
                PessoaEndCidade = infracaoPessoa.PessoaEndCidade,
                PessoaEndEstado = infracaoPessoa.PessoaEndEstado,
                LocalId = infracaoPessoa.LocalId,
                LocalCodigoMunicipio = infracaoPessoa.LocalCodigoMunicipio,
                LocalCep = infracaoPessoa.LocalCep,
                LocalMunicipio = infracaoPessoa.LocalMunicipio,
                LocalComplemento = infracaoPessoa.LocalComplemento,
                LocalBairro = infracaoPessoa.LocalBairro,
                LocalNumero = infracaoPessoa.LocalNumero,
                LocalEstado = infracaoPessoa.LocalEstado,
                LocalLogradouro = infracaoPessoa.LocalLogradouro,
                DataAbordagem = infracaoPessoa.DataAbordagem,
                NomeAgente = infracaoPessoa.NomeAgente,
                CpfAgente = infracaoPessoa.CpfAgente,
                AbordadoPeloOrgao = infracaoPessoa.AbordadoPeloOrgao,
                AbordadoPeloIdOrgao = infracaoPessoa.AbordadoPeloIdOrgao,
                AbordadoPeloOrgaoCompetencia = infracaoPessoa.AbordadoPeloOrgaoCompetencia,

                IdTipoInfracao = infracaoPessoa.IdTipoInfracao,
                ArtigoCodigo = infracaoPessoa.ArtigoCodigo,
                ArtigoDesdobramento = infracaoPessoa.ArtigoDesdobramento,
                ArtigoCompetencia = infracaoPessoa.ArtigoCompetencia,
                ArtigoNatureza = infracaoPessoa.ArtigoNatureza,
                Artigo = infracaoPessoa.Artigo,
                ArtigoInfrator = infracaoPessoa.ArtigoInfrator,
                ArtigoDescricao = infracaoPessoa.ArtigoDescricao,
                ArtigoDescricaoCompleta = infracaoPessoa.ArtigoDescricaoCompleta,
                ArtigoApreensaoVeiculo = infracaoPessoa.ArtigoApreensaoVeiculo,
                ArtigoSuspenderCNH = infracaoPessoa.ArtigoSuspenderCNH,
                ArtigoApresentaCondutor = infracaoPessoa.ArtigoApresentaCondutor,
                ArtigoRemoverVeiculo = infracaoPessoa.ArtigoRemoverVeiculo,
                ArtigoConfiscatePlates = infracaoPessoa.ArtigoConfiscatePlates,
                ArtigoCargaExcessiva = infracaoPessoa.ArtigoCargaExcessiva,
                ArtigoRequerDocumento = infracaoPessoa.ArtigoRequerDocumento,
                ArtigoRequerEquipamento = infracaoPessoa.ArtigoRequerEquipamento,
                ArtigoEmVigencia = infracaoPessoa.ArtigoEmVigencia,
                ArtigoInicioVigencia = infracaoPessoa.ArtigoInicioVigencia,
                ArtigoFinalVigencia = infracaoPessoa.ArtigoFinalVigencia,
                ArtigoPontos = infracaoPessoa.ArtigoPontos,
                ArtigoEquipamento = infracaoPessoa.ArtigoEquipamento,
                ArtigoValorMulta = infracaoPessoa.ArtigoValorMulta
            });

            return id;
        }

        public async Task<int> InserirInfracaoUsuarioDispositivo(string idDispositivo, string sequencia)
        {
            string sql = $@"
            INSERT INTO Inf_TalonarioDispositivo (IdDispositivo, Sequencia)
            OUTPUT INSERTED.Id --OUTPUT INSERTED.*
            VALUES (
                @IdDispositivo, @Sequencia
            ) --SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _connection.QuerySingleAsync<int>(sql, new
            {
                IdDispositivo = idDispositivo,
                Sequencia = sequencia
            });

            return id;
        }

        public async Task<IEnumerable<TipoInfracaoEntity>> ObterDicionarioDeTiposDeInfracoes()
        {
            string sql = $@"
                SELECT
                    ti.IdTipoInfracao,
                    ti.CodigoInfracao,
                    ti.Desdobramento,
                    c.Descricao as 'Competencia',
                    ti.BaseLegal as 'Artigo',
                    r.Descricao as 'Infrator',
                    n.Pontos,
                    nv.Valor,
                    n.Descricao as Natureza,
                    ti.DescricaoResumida,
                    ti.DescricaoCompleta,
                    timm.Descricao as 'Equipamento',
                    timm.CodigoTipoInstrumento as 'CodigoEquipamento',
                    ti.CadastroDeAnexos as 'RequerAnexo',
                    ti.CadastroDeEquipamento as 'RequerEquipamento',
                    ti.RetemVeiculo,
                    ti.ApresentaCondutor,
                    ti.ApreensaoPlaca,
                    ti.TransbordoCarga,
                    ti.ApreensaoVeiculo,
                    ti.SuspensaoCarteira,
                    ti.DataIniVigencia,
                    ti.DataFimVigencia,
                    ti.DataInclusao,
                    ti.Ativo
                FROM Inf_TipoInfracao ti
                INNER JOIN Competencia c on ti.IdCompetencia = c.Id
                INNER JOIN Inf_NaturezaInfracao n on ti.CodigoNatureza = n.CodigoNatureza
                INNER JOIN inf_NaturezaInfracaoValor nv on ti.CodigoNatureza = nv.CodigoNatureza
                INNER JOIN Responsabilidade r on ti.Responsabilidade = r.Id
                LEFT JOIN Inf_TipoInstrumento timm on ti.IdEquipamento = timm.CodigoTipoInstrumento";

            var result = await _connection.QueryAsync<TipoInfracaoEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<IEnumerable<EquipamentoEntity>> ObterEquipamentosDeRegistroDeInfracoes()
        {
            string sql = $@"
                SELECT
	                ti.CodigoTipoInstrumento as 'CodigoEquipamento',
	                ti.Descricao as 'Equipamento',
	                tm.SiglaMedicao as 'UnidadeDeMedida',
	                timm.IdMarcaModeloInstrumento,
	                timm.Marca
                FROM Inf_TipoInstrumento ti
                INNER JOIN Inf_TipoMedicao tm ON ti.TipoMedicao = tm.TipoMedicao
                LEFT JOIN inf_TipoInstrumentoMarcaModelo timm ON timm.CodigoTipoInstrumento = ti.CodigoTipoInstrumento";

            var result = await _connection.QueryAsync<EquipamentoEntity>(sql, new { });

            return result.ToList();
        }

        public async Task<IEnumerable<EquipamentoEntity>> ObterEquipamentosDeRegistroDeInfracoesPorInfracao(int codigoInfracao)
        {
            string sql = $@"
                SELECT
                    ti.CodigoInfracao,
	                tins.CodigoTipoInstrumento as 'CodigoEquipamento',
	                tins.Descricao as 'Equipamento',
	                tm.SiglaMedicao as 'UnidadeDeMedida',
	                timm.IdMarcaModeloInstrumento,
	                timm.Marca
                FROM Inf_TipoInfracao ti
                INNER JOIN Inf_TipoInstrumento tins ON ti.IdEquipamento = tins.CodigoTipoInstrumento
                INNER JOIN Inf_TipoMedicao tm ON tins.TipoMedicao = tm.TipoMedicao
                LEFT JOIN Inf_TipoInstrumentoMarcaModelo timm on ti.IdEquipamento = tins.CodigoTipoInstrumento
                WHERE
                    ti.CodigoInfracao = @CodigoInfracao";

            var result = await _connection.QueryAsync<EquipamentoEntity>(sql, new
            {
                CodigoInfracao = codigoInfracao
            });

            return result.ToList();
        }

        public async Task<IEnumerable<InfracaoAnexoEntity>> ObterInfracaoAnexosPorIdInfracao(string ait)
        {
            string sql = $@"
            SELECT
                ia.Id,
                ia.AIT,
                ia.AnexoBase64
            FROM Inf_InfracaoAnexo ia
            WHERE ia.AIT = @AIT";

            var result = await _connection.QueryAsync<InfracaoAnexoEntity>(sql, new { AIT = ait });
            return result.ToList();
        }

        public async Task<TalonarioDispositivoEntity> ObterInfracaoUsuarioDispositivoPorIdDispositivo(string idDispositivo)
        {
            string sql = $@"
            SELECT
                td.Id,
                td.IdDispositivo,
                td.Sequencia
            FROM Inf_TalonarioDispositivo td
            WHERE td.IdDispositivo = @IdDispositivo";

            var result = await _connection.QuerySingleOrDefaultAsync<TalonarioDispositivoEntity>(sql, new
            {
                IdDispositivo = idDispositivo,
            });

            return result;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidas()
        {
            string sql = $@"
                        SELECT
                            Id,
                            AIT,
                            JSON,
                            Tipo,
                            DataCancelamento,
                            DataEnviado
                        FROM Inf_InfracaoNaoTransmitida
                        WHERE
                            (Tipo = 'veiculo' OR Tipo IS NULL)
                            AND DataCancelamento IS NULL
                            AND (
                                DataEnviado >= DATEADD(DAY, -1, GETDATE())
                                OR (
                                    DataEnviado IS NULL
                                    AND TRY_CAST(JSON_VALUE(JSON, '$.dataAplicacao') AS DATETIME2) >= DATEADD(DAY, -5, GETDATE())
                                )
                            )
                        ORDER BY Id DESC;";

            var result = await _connection.QueryAsync<InfracaoNaoTransmitidaEntity>(sql, new { });

            return result;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidasComChassiSemPlaca()
        {
            string sql = $@"
                SELECT
	                Id,
	                AIT,
	                JSON,
	                Tipo,
	                DataCancelamento,
	                DataEnviado
                FROM Inf_InfracaoNaoTransmitida
                WHERE JSON_VALUE(JSON,'$.veiculoChassi') IS NOT NULL
                AND JSON_VALUE(JSON,'$.veiculoPlaca') = ''
                AND DataEnviado IS NULL
                AND DataCancelamento IS NULL";

            var result = await _connection.QueryAsync<InfracaoNaoTransmitidaEntity>(sql, new { });

            return result;
        }

        public IEnumerable<InfracaoNaoTransmitidaEntity> ObterInfracoesNaoTransmitidasComChassiSemPlaca2()
        {
            string sql = $@"
                SELECT
	                Id,
	                AIT,
	                JSON,
	                Tipo,
	                DataCancelamento,
	                DataEnviado
                FROM Inf_InfracaoNaoTransmitida
                WHERE JSON_VALUE(JSON,'$.veiculoChassi') IS NOT NULL
                AND JSON_VALUE(JSON,'$.veiculoPlaca') = ''
                AND DataEnviado IS NULL
                AND DataCancelamento IS NULL";

            var result = _connection.Query<InfracaoNaoTransmitidaEntity>(sql, new { });

            return result;
        }

        public async Task<IEnumerable<InfracaoNaoTransmitidaEntity>> ObterInfracoesNaoTransmitidasPessoas()
        {
            string sql = $@"
            SELECT
                Id,
                AIT,
                JSON,
                Tipo,
                DataCancelamento,
                DataEnviado
            FROM Inf_InfracaoNaoTransmitida
            WHERE Tipo = 'pedestre'
            ORDER BY Id DESC";

            var result = await _connection.QueryAsync<InfracaoNaoTransmitidaEntity>(sql, new { });

            return result;
        }

        public async Task<VeiculoEmplacadoViewModel> ObterPlacaPorChassi(string chassi)
        {
            string sql = $@"SELECT Placa, Chassi, DataInclusao FROM Rev_Veiculo WHERE Chassi LIKE @chassi";

            var result = await _connection.QueryFirstOrDefaultAsync<VeiculoEmplacadoViewModel>(sql, new
            {
                chassi = chassi,
            });

            return result;
        }

        public VeiculoEmplacadoViewModel ObterPlacaPorChassi2(string chassi)
        {
            string sql = $@"SELECT Placa, Chassi, DataInclusao FROM Rev_Veiculo WHERE Chassi LIKE @chassi";

            var result = _connection.QueryFirstOrDefault<VeiculoEmplacadoViewModel>(sql, new
            {
                chassi = chassi,
            });

            return result;
        }

        public async Task<bool> RemoverInfracaoAnexo(string ait)
        {
            string sql = $@"
            DELETE FROM Inf_InfracaoAnexo
            WHERE AIT = @AIT";

            var result = await _connection.ExecuteAsync(sql, new { AIT = ait });
            return result > 0;
        }

        public async Task<bool> RemoverInfracaoNaoTransmitidaPorAIT(string ait)
        {
            string sql = $@"
            DELETE FROM Inf_InfracaoNaoTransmitida
            WHERE AIT = @AIT";

            var result = await _connection.ExecuteAsync(sql, new
            {
                AIT = ait,
            });

            return result > 0;
        }

        public async Task<bool> RemoverInfracaoPdf(string ait)
        {
            string sql = $@"
            DELETE FROM Inf_AutoInfracaoPDF
            WHERE ait = @ait";

            var result = await _connection.ExecuteAsync(sql, new { ait = ait });
            return result > 0;
        }

        public async Task<string> stp_Inf_Ws_AutoInfracaoEletronica_ins(InfracaoEntity infracao)
        {
            string sql = $@"
            stp_Inf_Ws_AutoInfracaoEletronica_ins_talonario
		        @IdSessao = -1,
                @TipoEntradaDados = 'E',
                @UsuarioInclusao = 'Talonário Eletrônico',
                @Placa = @_Placa,
                @UfPlaca = @_UfPlaca,
                @Chassi = @_Chassi,
                @CodLocalVeiculo = @_CodLocalVeiculo,
                @CodigoOrgaoAutuador = @_CodigoOrgaoAutuador,
                @CodigoInfracao = @_CodigoInfracao,
                @Desdobramento = @_Desdobramento,
                @NumeroAuto = @_NumeroAuto,
                @DataAuto = @_DataAuto,
                @HoraAuto = @_HoraAuto,
                @CodigoMunicipio = @_CodigoMunicipio,
                @DataLote = @_DataLote,
                @NumeroTama = @_NumeroTama,
                @NumeroTermoConstatacao = @_NumeroTermoConstatacao,
                @RecolhePPD = @_RecolhePPD,
                @RecolheCNH = @_RecolheCNH,
                @RecolheACC = @_RecolheACC,
                @RecolheCLRV = @_RecolheCLRV,
                @RecolheCRV = @_RecolheCRV,
                @RecolheOutros = @_RecolheOutros,
                @RecolheOutrosDados = @_RecolheOutrosDados,
                @VeiculoRemovido = @_VeiculoRemovido,
                @VeiculoRetido = @_VeiculoRetido,
                @VeiculoOutros = @_VeiculoOutros,
                @VeiculoOutrosDados = @_VeiculoOutrosDados,
                @CodigoTipoInstrumento = @_CodigoTipoInstrumento,
                @InstrumentoAfericao = @_InstrumentoAfericao,
                @idMarcaModeloInstrumento = @_idMarcaModeloInstrumento,
                @NumeroSerieInstrumento = @_NumeroSerieInstrumento,
                @NumeroTesteInstrumento = @_NumeroTesteInstrumento,
                @DataAfericao = @_DataAfericao,
                @MedicaoRegistrada = @_MedicaoRegistrada,
                @MedicaoPermitida = @_MedicaoPermitida,
                @CpfAgente = @_CpfAgente,
                @MatriculaAgente = @_MatriculaAgente,
                @Observacao = @_Observacao,
                @NomeAgente = @_NomeAgente,
                @Local = @_Local,
                @NumeroLocal = @_NumeroLocal,
                @Complemento = @_Complemento,
                @BairroInfracao = @_BairroInfracao,
                @IndicadorAssinatura = @_IndicadorAssinatura,
                @NomeCondutor = @_NomeCondutor,
                @CPFCondutor = @_CPFCondutor,
                @TipoDocumentoCondutor = @_TipoDocumentoCondutor,
                @NumeroCNH = @_NumeroCNH,
                @UFCNH = @_UFCNH,
                @NumeroLote = NULL,
                @idRegistroLote = NULL,
                @MotivoRejeicao = NULL,
                @idTransacao = NULL,
                @Abordagem = @_Abordagem";

            var param = new
            {
                _IdSessao = -1,
                _Placa = infracao.Placa,
                _UfPlaca = infracao.UfPlaca,
                _Chassi = infracao.Chassi,
                _CodLocalVeiculo = infracao.CodLocalVeiculo,
                _CodigoOrgaoAutuador = infracao.CodigoOrgaoAutuador,
                _CodigoInfracao = infracao.CodigoInfracao,
                _Desdobramento = infracao.Desdobramento,
                _NumeroAuto = infracao.NumeroAuto,
                _DataAuto = infracao.DataAuto,
                _HoraAuto = infracao.HoraAuto,
                _CodigoMunicipio = infracao.CodigoMunicipio,
                _DataLote = infracao.DataLote,
                _NumeroTama = infracao.NumeroTama,
                _NumeroTermoConstatacao = infracao.NumeroTermoConstatacao,
                _RecolhePPD = infracao.RecolhePPD,
                _RecolheCNH = infracao.RecolheCNH,
                _RecolheACC = infracao.RecolheACC,
                _RecolheCLRV = infracao.RecolheCLRV,
                _RecolheCRV = infracao.RecolheCRV,
                _RecolheOutros = infracao.RecolheOutros,
                _RecolheOutrosDados = infracao.RecolheOutrosDados,
                _VeiculoRemovido = infracao.VeiculoRemovido,
                _VeiculoRetido = infracao.VeiculoRetido,
                _VeiculoOutros = infracao.VeiculoOutros,
                _VeiculoOutrosDados = infracao.VeiculoOutrosDados,
                _CodigoTipoInstrumento = infracao.CodigoTipoInstrumento,
                _InstrumentoAfericao = infracao.InstrumentoAfericao,
                _idMarcaModeloInstrumento = infracao.idMarcaModeloInstrumento,
                _NumeroSerieInstrumento = infracao.NumeroSerieInstrumento,
                _NumeroTesteInstrumento = infracao.NumeroTesteInstrumento,
                _DataAfericao = infracao.DataAfericao,
                _MedicaoRegistrada = infracao.MedicaoRegistrada,
                _MedicaoPermitida = infracao.MedicaoPermitida,
                _CpfAgente = infracao.CpfAgente,
                _MatriculaAgente = infracao.MatriculaAgente,
                _Observacao = infracao.Observacao,
                _NomeAgente = infracao.NomeAgente,
                _Local = infracao.Local,
                _NumeroLocal = infracao.NumeroLocal,
                _Complemento = infracao.Complemento,
                _BairroInfracao = infracao.BairroInfracao,
                _IndicadorAssinatura = infracao.IndicadorAssinatura,
                _NomeCondutor = infracao.NomeCondutor,
                _CPFCondutor = infracao.CPFCondutor,
                _TipoDocumentoCondutor = infracao.TipoDocumentoCondutor,
                _NumeroCNH = infracao.NumeroCNH,
                _UFCNH = infracao.UFCNH,
                _Abordagem = infracao.Abordagem
            };

            return await _connection.QuerySingleAsync<string>(sql, param);
        }

        public string stp_Inf_Ws_AutoInfracaoEletronica_ins2(InfracaoEntity infracao)
        {
            try
            {
                string sql = $@"
            stp_Inf_Ws_AutoInfracaoEletronica_ins_talonario
		        @IdSessao = -1,
                @TipoEntradaDados = 'E',
                @UsuarioInclusao = 'Talonário Eletrônico',
                @Placa = @_Placa,
                @UfPlaca = @_UfPlaca,
                @Chassi = @_Chassi,
                @CodLocalVeiculo = @_CodLocalVeiculo,
                @CodigoOrgaoAutuador = @_CodigoOrgaoAutuador,
                @CodigoInfracao = @_CodigoInfracao,
                @Desdobramento = @_Desdobramento,
                @NumeroAuto = @_NumeroAuto,
                @DataAuto = @_DataAuto,
                @HoraAuto = @_HoraAuto,
                @CodigoMunicipio = @_CodigoMunicipio,
                @DataLote = @_DataLote,
                @NumeroTama = @_NumeroTama,
                @NumeroTermoConstatacao = @_NumeroTermoConstatacao,
                @RecolhePPD = @_RecolhePPD,
                @RecolheCNH = @_RecolheCNH,
                @RecolheACC = @_RecolheACC,
                @RecolheCLRV = @_RecolheCLRV,
                @RecolheCRV = @_RecolheCRV,
                @RecolheOutros = @_RecolheOutros,
                @RecolheOutrosDados = @_RecolheOutrosDados,
                @VeiculoRemovido = @_VeiculoRemovido,
                @VeiculoRetido = @_VeiculoRetido,
                @VeiculoOutros = @_VeiculoOutros,
                @VeiculoOutrosDados = @_VeiculoOutrosDados,
                @CodigoTipoInstrumento = @_CodigoTipoInstrumento,
                @InstrumentoAfericao = @_InstrumentoAfericao,
                @idMarcaModeloInstrumento = @_idMarcaModeloInstrumento,
                @NumeroSerieInstrumento = @_NumeroSerieInstrumento,
                @NumeroTesteInstrumento = @_NumeroTesteInstrumento,
                @DataAfericao = @_DataAfericao,
                @MedicaoRegistrada = @_MedicaoRegistrada,
                @MedicaoPermitida = @_MedicaoPermitida,
                @CpfAgente = @_CpfAgente,
                @MatriculaAgente = @_MatriculaAgente,
                @Observacao = @_Observacao,
                @NomeAgente = @_NomeAgente,
                @Local = @_Local,
                @NumeroLocal = @_NumeroLocal,
                @Complemento = @_Complemento,
                @BairroInfracao = @_BairroInfracao,
                @IndicadorAssinatura = @_IndicadorAssinatura,
                @NomeCondutor = @_NomeCondutor,
                @CPFCondutor = @_CPFCondutor,
                @TipoDocumentoCondutor = @_TipoDocumentoCondutor,
                @NumeroCNH = @_NumeroCNH,
                @UFCNH = @_UFCNH,
                @NumeroLote = NULL,
                @idRegistroLote = NULL,
                @MotivoRejeicao = NULL,
                @idTransacao = NULL,
                @Abordagem = @_Abordagem";

                var param = new
                {
                    _IdSessao = -1,
                    _Placa = infracao.Placa,
                    _UfPlaca = infracao.UfPlaca,
                    _Chassi = infracao.Chassi,
                    _CodLocalVeiculo = infracao.CodLocalVeiculo,
                    _CodigoOrgaoAutuador = infracao.CodigoOrgaoAutuador,
                    _CodigoInfracao = infracao.CodigoInfracao,
                    _Desdobramento = infracao.Desdobramento,
                    _NumeroAuto = infracao.NumeroAuto,
                    _DataAuto = infracao.DataAuto,
                    _HoraAuto = infracao.HoraAuto,
                    _CodigoMunicipio = infracao.CodigoMunicipio,
                    _DataLote = infracao.DataLote,
                    _NumeroTama = infracao.NumeroTama,
                    _NumeroTermoConstatacao = infracao.NumeroTermoConstatacao,
                    _RecolhePPD = infracao.RecolhePPD,
                    _RecolheCNH = infracao.RecolheCNH,
                    _RecolheACC = infracao.RecolheACC,
                    _RecolheCLRV = infracao.RecolheCLRV,
                    _RecolheCRV = infracao.RecolheCRV,
                    _RecolheOutros = infracao.RecolheOutros,
                    _RecolheOutrosDados = infracao.RecolheOutrosDados,
                    _VeiculoRemovido = infracao.VeiculoRemovido,
                    _VeiculoRetido = infracao.VeiculoRetido,
                    _VeiculoOutros = infracao.VeiculoOutros,
                    _VeiculoOutrosDados = infracao.VeiculoOutrosDados,
                    _CodigoTipoInstrumento = infracao.CodigoTipoInstrumento,
                    _InstrumentoAfericao = infracao.InstrumentoAfericao,
                    _idMarcaModeloInstrumento = infracao.idMarcaModeloInstrumento,
                    _NumeroSerieInstrumento = infracao.NumeroSerieInstrumento,
                    _NumeroTesteInstrumento = infracao.NumeroTesteInstrumento,
                    _DataAfericao = infracao.DataAfericao,
                    _MedicaoRegistrada = infracao.MedicaoRegistrada,
                    _MedicaoPermitida = infracao.MedicaoPermitida,
                    _CpfAgente = infracao.CpfAgente,
                    _MatriculaAgente = infracao.MatriculaAgente,
                    _Observacao = infracao.Observacao,
                    _NomeAgente = infracao.NomeAgente,
                    _Local = infracao.Local,
                    _NumeroLocal = infracao.NumeroLocal,
                    _Complemento = infracao.Complemento,
                    _BairroInfracao = infracao.BairroInfracao,
                    _IndicadorAssinatura = infracao.IndicadorAssinatura,
                    _NomeCondutor = infracao.NomeCondutor,
                    _CPFCondutor = infracao.CPFCondutor,
                    _TipoDocumentoCondutor = infracao.TipoDocumentoCondutor,
                    _NumeroCNH = infracao.NumeroCNH,
                    _UFCNH = infracao.UFCNH,
                    _Abordagem = infracao.Abordagem
                };

                return _connection.QuerySingle<string>(sql, param);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                throw new Exception(a, ex);
            }
        }

        #endregion Public Methods
    }
}