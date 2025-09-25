using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using static Talonario.Api.Server.Application.Helpers.ConfigHelper;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class TamaRepository : ITamaRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TamaRepository> _logger;

        public TamaRepository(IConfiguration configuration, ILogger<TamaRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("AtelierDataBase");
            _logger = logger;
        }

        public async Task<TamaEntity> CadastrarTermoAdocaoMedidaAdministrativaAsync(TamaEntity entity)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var transaction = (SqlTransaction)await db.BeginTransactionAsync())
                {
                    try
                    {
                        if (entity.Id == null)
                        {
                            entity.CodigoVerificador = CodigoVerificador.Gerar(6);
                            entity.NumeroTAMA = Guid.NewGuid().ToString();

                            var result = await db.QuerySingleAsync<int>(@"
                        INSERT INTO [dbo].[Inf_TermoAdocaoMedidaAdministrativa]
                        (
                            [CepLocalRecolhimento], [EnderecoLocalRecolhimento],
                            [MunicipioUfLocalRecolhimento], [DataHoraLocalRecolhimento],
                            [LatitudeLocalRecolhimento], [LongitudeLocalRecolhimento],
                            [TransporteLocalRecolhimento], [PlacaVeiculo], [PaisVeiculo],
                            [MunicipioVeiculo], [UfVeiculo], [RenavamVeiculo],
                            [MarcaModeloVeiculo], [EspecieVeiculo], [CategoriaVeiculo],
                            [CorVeiculo], [NomeCondutor], [CpfCondutor], [RgCondutor],
                            [CnhCondutor], [TelefoneCondutor], [CepCondutor],
                            [EnderecoCondutor], [MunicipioUfCondutor], [NomeCondutorEntregue],
                            [CpfCondutorEntregue], [RgCondutorEntregue], [CnhCondutorEntregue],
                            [TelefoneCondutorEntregue], [CepCondutorEntregue],
                            [EnderecoCondutorEntregue], [MunicipioUfCondutorEntregue],
                            [Observacoes], [MatriculaAgente], [MatriculaTestemunha1],
                            [MatriculaTestemunha2], [EstadoGeralLatariaPintura],
                            [EquipamentosObrigatoriosAusentes], [ObjetosEncontradosVeiculo],
                            [VeiculoEntregueComChave], [DataHoraInclusao], [UsuarioInclusao],
                            [CodigoVerificador], [NumeroCondutor], [BairroCondutor],
                            [NumeroLocalInfracao], [BairroLocalInfracao], [Chassi],
                            [VeiculoEmplacado], [DataHoraAssinou], [MatriculaAssinou],
                            [DataHoraCancelou], [MatriculaCancelou], [ObservacoesAdicionais],[NumeroReciboRetencaoTalonario]

                        )
                        VALUES
                        (
                            @CepLocalRecolhimento, @EnderecoLocalRecolhimento,
                            @MunicipioUfLocalRecolhimento, @DataHoraLocalRecolhimento,
                            @LatitudeLocalRecolhimento, @LongitudeLocalRecolhimento,
                            @TransporteLocalRecolhimento, @PlacaVeiculo, @PaisVeiculo,
                            @MunicipioVeiculo, @UfVeiculo, @RenavamVeiculo,
                            @MarcaModeloVeiculo, @EspecieVeiculo, @CategoriaVeiculo,
                            @CorVeiculo, @NomeCondutor, @CpfCondutor, @RgCondutor,
                            @CnhCondutor, @TelefoneCondutor, @CepCondutor,
                            @EnderecoCondutor, @MunicipioUfCondutor, @NomeCondutorEntregue,
                            @CpfCondutorEntregue, @RgCondutorEntregue, @CnhCondutorEntregue,
                            @TelefoneCondutorEntregue, @CepCondutorEntregue,
                            @EnderecoCondutorEntregue, @MunicipioUfCondutorEntregue,
                            @Observacoes, @MatriculaAgente, @MatriculaTestemunha1,
                            @MatriculaTestemunha2, @EstadoGeralLatariaPintura,
                            @EquipamentosObrigatoriosAusentes, @ObjetosEncontradosVeiculo,
                            @VeiculoEntregueComChave, GETDATE(), @UsuarioInclusao,
                            @CodigoVerificador, @NumeroCondutor, @BairroCondutor,
                            @NumeroLocalInfracao, @BairroLocalInfracao, @Chassi,
                            @VeiculoEmplacado, @DataHoraAssinou, @MatriculaAssinou,
                            @DataHoraCancelou, @MatriculaCancelou, @ObservacoesAdicionais, @NumeroReciboRetencaoTalonario
                        );
                        SELECT CAST(SCOPE_IDENTITY() as int)", entity, transaction);

                            entity.Id = result;

                            var numeroTAMA = $"MA{DateTime.Now.Year}.{entity.Id.Value.ToString("D6")}";
                            await db.ExecuteAsync(@"
                        UPDATE [dbo].[Inf_TermoAdocaoMedidaAdministrativa]
                        SET NumeroTAMA = @NumeroTAMA
                        WHERE Id = @Id",
                                    new { NumeroTAMA = numeroTAMA, Id = entity.Id.Value }, transaction);

                            entity.NumeroTAMA = numeroTAMA;
                        }
                        else
                        {
                            await db.ExecuteAsync(@"
                        UPDATE [dbo].[Inf_TermoAdocaoMedidaAdministrativa]
                        SET
                            [CepLocalRecolhimento] = @CepLocalRecolhimento,
                            [EnderecoLocalRecolhimento] = @EnderecoLocalRecolhimento,
                            [MunicipioUfLocalRecolhimento] = @MunicipioUfLocalRecolhimento,
                            [DataHoraLocalRecolhimento] = @DataHoraLocalRecolhimento,
                            [LatitudeLocalRecolhimento] = @LatitudeLocalRecolhimento,
                            [LongitudeLocalRecolhimento] = @LongitudeLocalRecolhimento,
                            [TransporteLocalRecolhimento] = @TransporteLocalRecolhimento,
                            [PlacaVeiculo] = @PlacaVeiculo,
                            [PaisVeiculo] = @PaisVeiculo,
                            [MunicipioVeiculo] = @MunicipioVeiculo,
                            [UfVeiculo] = @UfVeiculo,
                            [RenavamVeiculo] = @RenavamVeiculo,
                            [MarcaModeloVeiculo] = @MarcaModeloVeiculo,
                            [EspecieVeiculo] = @EspecieVeiculo,
                            [CategoriaVeiculo] = @CategoriaVeiculo,
                            [CorVeiculo] = @CorVeiculo,
                            [NomeCondutor] = @NomeCondutor,
                            [CpfCondutor] = @CpfCondutor,
                            [RgCondutor] = @RgCondutor,
                            [CnhCondutor] = @CnhCondutor,
                            [TelefoneCondutor] = @TelefoneCondutor,
                            [CepCondutor] = @CepCondutor,
                            [EnderecoCondutor] = @EnderecoCondutor,
                            [MunicipioUfCondutor] = @MunicipioUfCondutor,
                            [NomeCondutorEntregue] = @NomeCondutorEntregue,
                            [CpfCondutorEntregue] = @CpfCondutorEntregue,
                            [RgCondutorEntregue] = @RgCondutorEntregue,
                            [CnhCondutorEntregue] = @CnhCondutorEntregue,
                            [TelefoneCondutorEntregue] = @TelefoneCondutorEntregue,
                            [CepCondutorEntregue] = @CepCondutorEntregue,
                            [EnderecoCondutorEntregue] = @EnderecoCondutorEntregue,
                            [MunicipioUfCondutorEntregue] = @MunicipioUfCondutorEntregue,
                            [Observacoes] = @Observacoes,
                            [MatriculaAgente] = @MatriculaAgente,
                            [MatriculaTestemunha1] = @MatriculaTestemunha1,
                            [MatriculaTestemunha2] = @MatriculaTestemunha2,
                            [EstadoGeralLatariaPintura] = @EstadoGeralLatariaPintura,
                            [EquipamentosObrigatoriosAusentes] = @EquipamentosObrigatoriosAusentes,
                            [ObjetosEncontradosVeiculo] = @ObjetosEncontradosVeiculo,
                            [VeiculoEntregueComChave] = @VeiculoEntregueComChave,
                            [NumeroCondutor] = @NumeroCondutor,
                            [BairroCondutor] = @BairroCondutor,
                            [NumeroLocalInfracao] = @NumeroLocalInfracao,
                            [BairroLocalInfracao] = @BairroLocalInfracao,
                            [Chassi] = @Chassi,
                            [VeiculoEmplacado] = @VeiculoEmplacado,
                            [DataHoraAssinou] = @DataHoraAssinou,
                            [MatriculaAssinou] = @MatriculaAssinou,
                            [DataHoraCancelou] = @DataHoraCancelou,
                            [MatriculaCancelou] = @MatriculaCancelou,
                            [ObservacoesAdicionais] = @ObservacoesAdicionais,
                            [NumeroReciboRetencaoTalonario] = @NumeroReciboRetencaoTalonario
                        WHERE [Id] = @Id", entity, transaction);
                        }

                        await ProcessarRelacionamentos(db, transaction, entity);
                        await transaction.CommitAsync();
                        return entity;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        _logger.LogError(ex, "Erro ao processar TAMA");
                        throw;
                    }
                }
            }
        }

        private async Task ProcessarRelacionamentos(SqlConnection db, DbTransaction transaction, TamaEntity entity)
        {
            await db.ExecuteAsync(@"DELETE [dbo].[Inf_TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentes]
                                  WHERE [IdTama] = @IdTama",
                                  new { IdTama = entity.Id.Value }, transaction);

            await db.ExecuteAsync(@"DELETE [dbo].[Inf_TermoAdocaoMedidaAdministrativa_DocumentosRecolhidos]
                                  WHERE [IdTama] = @IdTama",
                                  new { IdTama = entity.Id.Value }, transaction);

            await db.ExecuteAsync(@"DELETE [dbo].[Inf_TermoAdocaoMedidaAdministrativa_AutosInfracao]
                                  WHERE [IdTama] = @IdTama",
                                  new { IdTama = entity.Id.Value }, transaction);

            if (!entity.Id.HasValue)
                throw new Exception("O campo Id do Termo de Adoção está nulo.");

            if (entity.AutosInfracao != null && entity.AutosInfracao.Any())
            {
                foreach (var item in entity.AutosInfracao)
                {
                    try
                    {
                        Console.WriteLine($"Tentando inserir AutoInfracao: {item.Numero}, Tipo: {item.Tipo}");

                        await db.ExecuteAsync(@"
                        INSERT INTO [dbo].[Inf_TermoAdocaoMedidaAdministrativa_AutosInfracao]
                        ([IdTama], [Numero], [Tipo])
                        VALUES (@IdTama, @Numero, @Tipo)",
                         new
                         {
                             IdTama = entity.Id.Value,
                             item.Numero,
                             item.Tipo
                         }, transaction);
                    }
                    catch (Exception exItem)
                    {
                        Console.WriteLine($"Erro ao inserir AutoInfracao: {exItem.Message}");
                        throw;
                    }
                }
            }

            if (entity.DocumentosRecolhidos != null)
            {
                foreach (var item in entity.DocumentosRecolhidos)
                {
                    await db.ExecuteAsync(@"
                        INSERT INTO [dbo].[Inf_TermoAdocaoMedidaAdministrativa_DocumentosRecolhidos]
                        ([IdTama], [Documento], [Numero])
                        VALUES (@IdTama, @Documento, @Numero)",
                        new
                        {
                            IdTama = entity.Id.Value,
                            item.Documento,
                            item.Numero
                        }, transaction);
                }
            }
            if (entity.EquipamentosObrigatoriosAusentesLista != null)
            {
                foreach (var item in entity.EquipamentosObrigatoriosAusentesLista)
                {
                    await db.ExecuteAsync(@"
            INSERT INTO [dbo].[Inf_TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentes]
            ([IdTama], [IdEquipamentoObrigatorio])
            VALUES (@IdTama, @IdEquipamentoObrigatorio)",
                        new
                        {
                            IdTama = entity.Id.Value,
                            item.IdEquipamentoObrigatorio
                        }, transaction);
                }
            }
        }
    }
}