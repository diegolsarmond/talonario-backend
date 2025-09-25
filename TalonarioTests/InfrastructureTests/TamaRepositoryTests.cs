using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.InfraStructure.Repository;
using TalonarioTests.InfrastructureTests.Fakes;
using Xunit;

namespace TalonarioTests.InfrastructureTests
{
    public class TamaRepositoryTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<ILogger<TamaRepository>> _loggerMock = new();

        public TamaRepositoryTests()
        {
            var settings = new Dictionary<string, string>
            {
                { "ConnectionStrings:AtelierDataBase", "Fake" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();
        }

        [Fact]
        public async Task SyncingWithNullAutosDoesNotIssueDeleteCommands()
        {
            var fakeConnection = new FakeDbConnection();
            var repository = new TamaRepository(_configuration, _loggerMock.Object, () => fakeConnection);
            var entity = CreateBaseEntity();
            entity.AutosInfracao = null;

            await repository.CadastrarTermoAdocaoMedidaAdministrativaAsync(entity);

            Assert.DoesNotContain(fakeConnection.ExecutedCommands,
                cmd => cmd.CommandText.Contains("Inf_TermoAdocaoMedidaAdministrativa_AutosInfracao", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task SyncingWithEmptyAutosListDeletesWithoutReinserting()
        {
            var fakeConnection = new FakeDbConnection();
            var repository = new TamaRepository(_configuration, _loggerMock.Object, () => fakeConnection);
            var entity = CreateBaseEntity();
            entity.AutosInfracao = new List<TermoAdocaoMedidaAdministrativa_AutosInfracao>();

            await repository.CadastrarTermoAdocaoMedidaAdministrativaAsync(entity);

            var autosCommands = fakeConnection.ExecutedCommands
                .Where(cmd => cmd.CommandText.Contains("Inf_TermoAdocaoMedidaAdministrativa_AutosInfracao", StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.Single(autosCommands);
            Assert.StartsWith("DELETE", autosCommands[0].CommandText.Trim(), StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain(fakeConnection.ExecutedCommands,
                cmd => cmd.CommandText.TrimStart().StartsWith("INSERT INTO [dbo].[Inf_TermoAdocaoMedidaAdministrativa_AutosInfracao]", StringComparison.OrdinalIgnoreCase));
        }

        private static TamaEntity CreateBaseEntity()
        {
            return new TamaEntity
            {
                Id = 1,
                NumeroTAMA = "MA2024.000001",
                NumeroReciboRetencaoTalonario = string.Empty,
                CepLocalRecolhimento = "12345678",
                EnderecoLocalRecolhimento = "Rua Teste",
                MunicipioUfLocalRecolhimento = "Cidade/UF",
                DataHoraLocalRecolhimento = DateTime.UtcNow,
                LatitudeLocalRecolhimento = string.Empty,
                LongitudeLocalRecolhimento = string.Empty,
                TransporteLocalRecolhimento = "Caminhao",
                PlacaVeiculo = "ABC1D23",
                PaisVeiculo = "BR",
                MunicipioVeiculo = "Cidade",
                UfVeiculo = "SP",
                RenavamVeiculo = string.Empty,
                MarcaModeloVeiculo = "Modelo",
                EspecieVeiculo = "Especie",
                CategoriaVeiculo = "Categoria",
                CorVeiculo = "Azul",
                NomeCondutor = "Condutor",
                CpfCondutor = "00000000000",
                RgCondutor = string.Empty,
                CnhCondutor = string.Empty,
                TelefoneCondutor = string.Empty,
                CepCondutor = "12345678",
                EnderecoCondutor = "Rua Teste",
                MunicipioUfCondutor = "Cidade/UF",
                NomeCondutorEntregue = string.Empty,
                CpfCondutorEntregue = string.Empty,
                RgCondutorEntregue = string.Empty,
                CnhCondutorEntregue = string.Empty,
                TelefoneCondutorEntregue = string.Empty,
                CepCondutorEntregue = "12345678",
                EnderecoCondutorEntregue = "Rua Teste",
                MunicipioUfCondutorEntregue = "Cidade/UF",
                Observacoes = string.Empty,
                MatriculaAgente = "123",
                MatriculaTestemunha1 = string.Empty,
                MatriculaTestemunha2 = string.Empty,
                EstadoGeralLatariaPintura = string.Empty,
                EquipamentosObrigatoriosAusentes = false,
                ObjetosEncontradosVeiculo = string.Empty,
                VeiculoEntregueComChave = true,
                DataHoraInclusao = DateTime.UtcNow,
                UsuarioInclusao = "tester",
                DataHoraAssinou = null,
                MatriculaAssinou = string.Empty,
                DataHoraCancelou = null,
                MatriculaCancelou = string.Empty,
                CodigoVerificador = string.Empty,
                ObservacoesAdicionais = string.Empty,
                NumeroCondutor = string.Empty,
                BairroCondutor = string.Empty,
                NumeroLocalInfracao = string.Empty,
                BairroLocalInfracao = string.Empty,
                Chassi = string.Empty,
                VeiculoEmplacado = 'S'
            };
        }
    }
}
