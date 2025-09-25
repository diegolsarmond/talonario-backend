using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using static Talonario.Api.Server.Application.Helpers.ConfigHelper;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class TermoRepository : ITermoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TermoRepository> _logger;
        private readonly Func<DbConnection> _connectionFactory;

        public TermoRepository(IConfiguration configuration, ILogger<TermoRepository> logger, Func<DbConnection> connectionFactory = null)
        {
            _connectionString = configuration.GetConnectionString("AtelierDataBase");
            _logger = logger;
            _connectionFactory = connectionFactory ?? (() => new SqlConnection(_connectionString));
        }

        public TermoConstatacao CadastrarTermoConstatacao(TermoConstatacao termo)
        {
            using (var connection = _connectionFactory())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (termo.Id == null)
                        {
                            termo.CodigoVerificador = CodigoVerificador.Gerar(6);

                            termo.Id = connection.QuerySingle<int>(@"
                                INSERT INTO [dbo].[Inf_TermoConstatacao]
                                ([NumeroTermoConstatacao],[NumeroTermoConstatacaoTalonario],[NomeCondutor],[CpfCondutor],[RgCondutor],[CnhCondutor],
                                [TelefoneCondutor],[CepCondutor],[EnderecoCondutor],[MunicipioUfCondutor],[PlacaVeiculo],
                                [PaisVeiculo],[MunicipioVeiculo],[UfVeiculo],[RenavamVeiculo],[MarcaModeloVeiculo],
                                [EspecieVeiculo],[CategoriaVeiculo],[CorVeiculo],[CepLocalInfracao],[EnderecoLocalInfracao],
                                [MunicipioUfLocalInfracao],[DataHoraLocalInfracao],[LatitudeLocalInfracao],[LongitudeLocalInfracao],
                                [Observacoes],[MatriculaAgente],[MatriculaTestemunha1],[MatriculaTestemunha2],[DataHoraInclusao],
                                [UsuarioInclusao],[DataHoraAssinou],[MatriculaAssinou],[DataHoraCancelou],[MatriculaCancelou],
                                [CodigoVerificador],[NumeroCondutor],[BairroCondutor],[NumeroLocalInfracao],[BairroLocalInfracao],
                                [CondicaoCondutor],[SubstanciaIdentificada],[TestesOferecidos],[Chassi],[VeiculoEmplacado],[Observacao])
                                VALUES
                                (('CP' + CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '.' +
                                REPLICATE('0', 6 - LEN(RTRIM(CAST(IDENT_CURRENT('dbo.Inf_TermoConstatacao') AS VARCHAR)))) +
                                RTRIM(CAST(IDENT_CURRENT('dbo.Inf_TermoConstatacao') AS VARCHAR))),
                                @NumeroTermoConstatacaoTalonario,
                                @NomeCondutor,@CpfCondutor,@RgCondutor,@CnhCondutor,@TelefoneCondutor,
                                @CepCondutor,@EnderecoCondutor,@MunicipioUfCondutor,@PlacaVeiculo,@PaisVeiculo,
                                @MunicipioVeiculo,@UfVeiculo,@RenavamVeiculo,@MarcaModeloVeiculo,@EspecieVeiculo,
                                @CategoriaVeiculo,@CorVeiculo,@CepLocalInfracao,@EnderecoLocalInfracao,@MunicipioUfLocalInfracao,
                                @DataHoraLocalInfracao,@LatitudeLocalInfracao,@LongitudeLocalInfracao,@Observacoes,@MatriculaAgente,
                                @MatriculaTestemunha1,@MatriculaTestemunha2,GETDATE(),@UsuarioInclusao,@DataHoraAssinou,
                                @MatriculaAssinou,@DataHoraCancelou,@MatriculaCancelou,@CodigoVerificador,@NumeroCondutor,
                                @BairroCondutor,@NumeroLocalInfracao,@BairroLocalInfracao,@CondicaoCondutor,@SubstanciaIdentificada,
                                @TestesOferecidos,@Chassi,@VeiculoEmplacado , @Observacao);
                                SELECT CAST(SCOPE_IDENTITY() as int)", termo, transaction);
                        }
                        else
                        {
                            connection.Execute(@"
                                UPDATE [dbo].[Inf_TermoConstatacao]
                                SET
                                [NumeroTermoConstatacaoTalonario] = @NumeroTermoConstatacaoTalonario,
                                [NomeCondutor]=@NomeCondutor,[CpfCondutor]=@CpfCondutor,[RgCondutor]=@RgCondutor,
                                [CnhCondutor]=@CnhCondutor,[TelefoneCondutor]=@TelefoneCondutor,[CepCondutor]=@CepCondutor,
                                [EnderecoCondutor]=@EnderecoCondutor,[MunicipioUfCondutor]=@MunicipioUfCondutor,
                                [PlacaVeiculo]=@PlacaVeiculo,[PaisVeiculo]=@PaisVeiculo,[MunicipioVeiculo]=@MunicipioVeiculo,
                                [UfVeiculo]=@UfVeiculo,[RenavamVeiculo]=@RenavamVeiculo,[MarcaModeloVeiculo]=@MarcaModeloVeiculo,
                                [EspecieVeiculo]=@EspecieVeiculo,[CategoriaVeiculo]=@CategoriaVeiculo,[CorVeiculo]=@CorVeiculo,
                                [CepLocalInfracao]=@CepLocalInfracao,[EnderecoLocalInfracao]=@EnderecoLocalInfracao,
                                [MunicipioUfLocalInfracao]=@MunicipioUfLocalInfracao,[DataHoraLocalInfracao]=@DataHoraLocalInfracao,
                                [LatitudeLocalInfracao]=@LatitudeLocalInfracao,[LongitudeLocalInfracao]=@LongitudeLocalInfracao,
                                [Observacoes]=@Observacoes,[MatriculaAgente]=@MatriculaAgente,
                                [MatriculaTestemunha1]=@MatriculaTestemunha1,[MatriculaTestemunha2]=@MatriculaTestemunha2,
                                [DataHoraAssinou]=@DataHoraAssinou,[MatriculaAssinou]=@MatriculaAssinou,
                                [DataHoraCancelou]=@DataHoraCancelou,[MatriculaCancelou]=@MatriculaCancelou,
                                [NumeroCondutor]=@NumeroCondutor,[BairroCondutor]=@BairroCondutor,
                                [NumeroLocalInfracao]=@NumeroLocalInfracao,[BairroLocalInfracao]=@BairroLocalInfracao,
                                [CondicaoCondutor]=@CondicaoCondutor,[SubstanciaIdentificada]=@SubstanciaIdentificada,
                                [TestesOferecidos]=@TestesOferecidos,[Chassi]=@Chassi,[VeiculoEmplacado]=@VeiculoEmplacado,[Observacao] = @Observacao
                                WHERE [Id]=@Id", termo, transaction);
                        }

                        if (termo.AutosInfracao != null)
                        {
                            connection.Execute(@"DELETE [dbo].[Inf_TermoConstatacao_AutosInfracao]
                                            WHERE [IdTermoConstatacao] = @Id",
                                            new { Id = termo.Id.Value }, transaction);

                            if (termo.AutosInfracao.Any())
                            {
                                foreach (var auto in termo.AutosInfracao)
                                {
                                    auto.IdTermoConstatacao = termo.Id.Value;
                                    auto.Id = connection.QuerySingle<int>(@"
                                INSERT INTO [dbo].[Inf_TermoConstatacao_AutosInfracao]
                                ([IdTermoConstatacao],[Numero],[Tipo])
                                VALUES (@IdTermoConstatacao,@Numero,@Tipo);
                                SELECT CAST(SCOPE_IDENTITY() as int)", auto, transaction);
                                }
                            }
                        }

                        connection.Execute(@"DELETE [dbo].[Inf_TermoConstatacao_AvaliacaoCondutor]
                                            WHERE [IdTermoConstatacao] = @Id",
                                            new { Id = termo.Id.Value }, transaction);

                        connection.Execute(@"DELETE [dbo].[Inf_TermoConstatacao_RelatosCondutor]
                                            WHERE [IdTermoConstatacao] = @Id",
                                            new { Id = termo.Id.Value }, transaction);

                        foreach (var relato in termo.RelatosCondutor)
                        {
                            relato.IdTermoConstatacao = termo.Id.Value;
                            relato.Id = connection.QuerySingle<int>(@"
                                INSERT INTO [dbo].[Inf_TermoConstatacao_RelatosCondutor]
                                ([IdTermoConstatacao],[IdDescricaoCondutor],[DescricaoCondutor],[DataHora])
                                VALUES (@IdTermoConstatacao,@IdDescricaoCondutor,@DescricaoCondutor,@DataHora);
                                SELECT CAST(SCOPE_IDENTITY() as int)", relato, transaction);
                        }

                        foreach (var avaliacao in termo.AvaliacaoCondutor)
                        {
                            avaliacao.IdTermoConstatacao = termo.Id.Value;
                            avaliacao.Id = connection.QuerySingle<int>(@"
                                INSERT INTO [dbo].[Inf_TermoConstatacao_AvaliacaoCondutor]
                                ([IdTermoConstatacao],[Descricao],[Tipo])
                                VALUES (@IdTermoConstatacao,@Descricao,@Tipo);
                                SELECT CAST(SCOPE_IDENTITY() as int)", avaliacao, transaction);
                        }

                        transaction.Commit();
                        return termo;
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Erro ao cadastrar termo de constatação");
                        throw new Exception("Falha ao cadastrar termo no banco de dados", ex);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Erro inesperado ao cadastrar termo");
                        throw;
                    }
                }
            }
        }

        public TermoConstatacao ConsultarTermoConstatacao(string numeroTc)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var termo = connection.QueryFirstOrDefault<TermoConstatacao>(@"
                        SELECT TOP 1 t.*,
                        a.Nome AS NomeAgente,
                        aa1.Nome AS NomeTestemunha1,
                        aa2.Nome AS NomeTestemunha2,
                        (CASE WHEN c.documento IS NULL THEN 0 ELSE 1 END) AS Comprovante
                        FROM [dbo].[Inf_TermoConstatacao] t
                        LEFT JOIN [dbo].[Inf_AgenteAutuador] aa ON aa.MatriculaAgente = t.MatriculaAgente
                        LEFT JOIN [dbo].[sys_UsuariosAPI] a ON a.IdPessoa = aa.IdOrigem
                        LEFT JOIN [dbo].[Ren_Pessoa] aa1 ON aa1.Cpf = t.MatriculaTestemunha1
                        LEFT JOIN [dbo].[Ren_Pessoa] aa2 ON aa2.Cpf = t.MatriculaTestemunha2
                        LEFT JOIN [dbo].[Inf_TermoConstatacao_Comprovante] c ON c.idtc = t.id
                        WHERE [NumeroTermoConstatacao] = @numeroTc", new { numeroTc });

                    if (termo != null)
                    {
                        termo.RelatosCondutor = connection.Query<RelatoCondutor>(
                            "SELECT * FROM Inf_TermoConstatacao_RelatosCondutor WHERE IdTermoConstatacao = @Id",
                            new { termo.Id }).ToList();

                        termo.AvaliacaoCondutor = connection.Query<AvaliacaoCondutor>(
                            "SELECT * FROM Inf_TermoConstatacao_AvaliacaoCondutor WHERE IdTermoConstatacao = @Id",
                            new { termo.Id }).ToList();

                        termo.AutosInfracao = connection.Query<AutoInfracao>(
                            "SELECT * FROM Inf_TermoConstatacao_AutosInfracao WHERE IdTermoConstatacao = @Id",
                            new { termo.Id }).ToList();
                    }

                    return termo;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao consultar termo {NumeroTermo}", numeroTc);
                throw new Exception("Falha ao consultar termo no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao consultar termo");
                throw;
            }
        }

        public IEnumerable<TermoConstatacao> ListarTermoConstatacao()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.Query<TermoConstatacao>(@"
                        SELECT TC.[Id], TC.[NumeroTermoConstatacao], TC.[NomeCondutor],
                        TC.[CpfCondutor], TC.[PlacaVeiculo], TC.[DataHoraAssinou],
                        TC.[MatriculaAssinou], TC.[DataHoraCancelou], TC.[MatriculaCancelou],
                        TC.[Observacoes], TC.[ObservacoesAdicionais],
                        (CASE WHEN C.documento IS NULL THEN 0 ELSE 1 END) Comprovante
                        FROM [dbo].[Inf_TermoConstatacao] TC
                        LEFT JOIN [dbo].[Inf_TermoConstatacao_Comprovante] C ON C.idtc = TC.Id
                        WHERE [NumeroTermoConstatacao] IS NOT NULL
                        ORDER BY [Id] DESC");
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao listar termos de constatação");
                throw new Exception("Falha ao listar termos no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao listar termos");
                throw;
            }
        }

        public IEnumerable<TermoConstatacao> PesquisarTermoConstatacao(string pesquisa)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.Query<TermoConstatacao>(@"
                        SELECT TC.[Id], TC.[NumeroTermoConstatacao], TC.[NomeCondutor],
                        TC.[CpfCondutor], TC.[PlacaVeiculo], TC.[DataHoraAssinou],
                        TC.[MatriculaAssinou], TC.[DataHoraCancelou], TC.[MatriculaCancelou],
                        TC.[Observacoes], TC.[ObservacoesAdicionais],
                        (CASE WHEN C.documento IS NULL THEN 0 ELSE 1 END) Comprovante
                        FROM [dbo].[Inf_TermoConstatacao] TC
                        LEFT JOIN [dbo].[Inf_TermoConstatacao_Comprovante] C ON C.idtc = TC.Id
                        WHERE [NumeroTermoConstatacao] IS NOT NULL
                        AND (TC.NumeroTermoConstatacao LIKE '%' + @pesquisa + '%'
                        OR TC.NomeCondutor LIKE '%' + @pesquisa + '%'
                        OR TC.CpfCondutor LIKE '%' + @pesquisa + '%'
                        OR TC.PlacaVeiculo LIKE '%' + @pesquisa + '%')",
                        new { pesquisa });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao pesquisar termos com pesquisa: {Pesquisa}", pesquisa);
                throw new Exception("Falha ao pesquisar termos no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao pesquisar termos");
                throw;
            }
        }

        public bool AssinarTermoConstatacao(TermoConstatacao termo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affected = connection.Execute(@"
                        UPDATE Inf_TermoConstatacao
                        SET DataHoraAssinou = @DataHoraAssinou,
                            MatriculaAssinou = @MatriculaAssinou
                        WHERE Id = @Id",
                        new { termo.DataHoraAssinou, termo.MatriculaAssinou, termo.Id });

                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao assinar termo {TermoId}", termo.Id);
                throw new Exception("Falha ao assinar termo no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao assinar termo");
                throw;
            }
        }

        public bool AdicionarObservacaoTermo(TermoConstatacao termo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affected = connection.Execute(@"
                        UPDATE [dbo].[Inf_TermoConstatacao]
                        SET ObservacoesAdicionais = @ObservacoesAdicionais
                        WHERE Id = @Id",
                        new { termo.ObservacoesAdicionais, termo.Id });

                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao adicionar observação ao termo {TermoId}", termo.Id);
                throw new Exception("Falha ao adicionar observação no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao adicionar observação");
                throw;
            }
        }

        public TermoConstatacao EditarTermoConstatacao(TermoConstatacao termo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    connection.Execute(@"
                        UPDATE [dbo].[Inf_TermoConstatacao]
                        SET [NomeCondutor] = @NomeCondutor,
                            [CpfCondutor] = @CpfCondutor,
                            [RgCondutor] = @RgCondutor,
                            [CnhCondutor] = @CnhCondutor,
                            [TelefoneCondutor] = @TelefoneCondutor,
                            [CepCondutor] = @CepCondutor,
                            [EnderecoCondutor] = @EnderecoCondutor,
                            [MunicipioUfCondutor] = @MunicipioUfCondutor,
                            [PlacaVeiculo] = @PlacaVeiculo,
                            [PaisVeiculo] = @PaisVeiculo,
                            [MunicipioVeiculo] = @MunicipioVeiculo,
                            [UfVeiculo] = @UfVeiculo,
                            [RenavamVeiculo] = @RenavamVeiculo,
                            [MarcaModeloVeiculo] = @MarcaModeloVeiculo,
                            [EspecieVeiculo] = @EspecieVeiculo,
                            [CategoriaVeiculo] = @CategoriaVeiculo,
                            [CorVeiculo] = @CorVeiculo,
                            [CepLocalInfracao] = @CepLocalInfracao,
                            [EnderecoLocalInfracao] = @EnderecoLocalInfracao,
                            [MunicipioUfLocalInfracao] = @MunicipioUfLocalInfracao,
                            [DataHoraLocalInfracao] = @DataHoraLocalInfracao,
                            [LatitudeLocalInfracao] = @LatitudeLocalInfracao,
                            [LongitudeLocalInfracao] = @LongitudeLocalInfracao,
                            [Observacoes] = @Observacoes,
                            [MatriculaAgente] = @MatriculaAgente,
                            [MatriculaTestemunha1] = @MatriculaTestemunha1,
                            [MatriculaTestemunha2] = @MatriculaTestemunha2,
                            [DataHoraAssinou] = @DataHoraAssinou,
                            [MatriculaAssinou] = @MatriculaAssinou,
                            [DataHoraCancelou] = @DataHoraCancelou,
                            [MatriculaCancelou] = @MatriculaCancelou,
                            [NumeroCondutor] = @NumeroCondutor,
                            [BairroCondutor] = @BairroCondutor,
                            [NumeroLocalInfracao] = @NumeroLocalInfracao,
                            [BairroLocalInfracao] = @BairroLocalInfracao,
                            [CondicaoCondutor] = @CondicaoCondutor,
                            [SubstanciaIdentificada] = @SubstanciaIdentificada,
                            [TestesOferecidos] = @TestesOferecidos
                        WHERE [Id] = @Id", termo);

                    return termo;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao editar termo {TermoId}", termo.Id);
                throw new Exception("Falha ao editar termo no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao editar termo");
                throw;
            }
        }

        public bool ExcluirTermoConstatacao(string numeroTc)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var termo = ConsultarTermoConstatacao(numeroTc);
                    if (termo == null) return false;

                    connection.Execute(@"
                        DELETE FROM [dbo].[Inf_TermoConstatacao]
                        WHERE [NumeroTermoConstatacao] = @numeroTc",
                        new { numeroTc });

                    return true;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao excluir termo {NumeroTermo}", numeroTc);
                throw new Exception("Falha ao excluir termo no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao excluir termo");
                throw;
            }
        }

        public bool CancelarTermoConstatacao(TermoConstatacao termo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affected = connection.Execute(@"
                        UPDATE [dbo].[Inf_TermoConstatacao]
                        SET DataHoraCancelou = GETDATE(),
                            MatriculaCancelou = @MatriculaCancelou
                        WHERE Id = @Id",
                        new { termo.MatriculaCancelou, termo.Id });

                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao cancelar termo {TermoId}", termo.Id);
                throw new Exception("Falha ao cancelar termo no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cancelar termo");
                throw;
            }
        }

        public int UploadComprovanteTermo(int idTermo, int comprovante)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.Execute(@"
                        INSERT INTO [Inf_TermoConstatacao_Comprovante]
                        (idtc, documento)
                        VALUES (@idTermo, @comprovante)",
                        new { idTermo, comprovante });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload de comprovante para termo {TermoId}", idTermo);
                throw new Exception("Falha ao fazer upload de comprovante no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao fazer upload de comprovante");
                throw;
            }
        }

        public bool RemoverComprovanteTermo(int idTermo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affected = connection.Execute(@"
                        DELETE FROM [Inf_TermoConstatacao_Comprovante]
                        WHERE idtc = @idTermo",
                        new { idTermo });

                    return affected > 0;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao remover comprovante do termo {TermoId}", idTermo);
                throw new Exception("Falha ao remover comprovante no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover comprovante");
                throw;
            }
        }

        public TermoConstatacao ObterComprovanteTermo(int idTermo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.QueryFirstOrDefault<TermoConstatacao>(@"
                        SELECT c.*
                        FROM [Inf_TermoConstatacao_Comprovante] c
                        WHERE c.idtc = @idTermo",
                        new { idTermo });
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Erro ao obter comprovante do termo {TermoId}", idTermo);
                throw new Exception("Falha ao obter comprovante no banco de dados", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao obter comprovante");
                throw;
            }
        }
    }
}