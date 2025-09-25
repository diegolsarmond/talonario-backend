using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.InfraStructure.Repository;
using TalonarioTests.InfrastructureTests.Fakes;
using Xunit;

namespace TalonarioTests.InfrastructureTests
{
    public class TermoRepositoryTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<ILogger<TermoRepository>> _loggerMock = new();

        public TermoRepositoryTests()
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
        public void UpdatingWithNullAutosKeepsExistingEntries()
        {
            var fakeConnection = new FakeDbConnection();
            var repository = new TermoRepository(_configuration, _loggerMock.Object, () => fakeConnection);
            var termo = CreateBaseEntity();
            termo.AutosInfracao = null;

            repository.CadastrarTermoConstatacao(termo);

            Assert.DoesNotContain(fakeConnection.ExecutedCommands,
                cmd => cmd.CommandText.Contains("Inf_TermoConstatacao_AutosInfracao", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void UpdatingWithEmptyAutosListDeletesWithoutReinserting()
        {
            var fakeConnection = new FakeDbConnection();
            var repository = new TermoRepository(_configuration, _loggerMock.Object, () => fakeConnection);
            var termo = CreateBaseEntity();
            termo.AutosInfracao = new List<AutoInfracao>();

            repository.CadastrarTermoConstatacao(termo);

            var autosCommands = fakeConnection.ExecutedCommands
                .Where(cmd => cmd.CommandText.Contains("Inf_TermoConstatacao_AutosInfracao", StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.Single(autosCommands);
            Assert.StartsWith("DELETE", autosCommands[0].CommandText.Trim(), StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain(fakeConnection.ExecutedCommands,
                cmd => cmd.CommandText.TrimStart().StartsWith("INSERT INTO [dbo].[Inf_TermoConstatacao_AutosInfracao]", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void UpdatingWithAutosReinsertsAfterDelete()
        {
            var fakeConnection = new FakeDbConnection();
            var repository = new TermoRepository(_configuration, _loggerMock.Object, () => fakeConnection);
            var termo = CreateBaseEntity();
            termo.AutosInfracao = new List<AutoInfracao>
            {
                new AutoInfracao { Numero = "123", Tipo = "Tipo" }
            };

            repository.CadastrarTermoConstatacao(termo);

            var autosCommands = fakeConnection.ExecutedCommands
                .Where(cmd => cmd.CommandText.Contains("Inf_TermoConstatacao_AutosInfracao", StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.Equal(2, autosCommands.Count);
            Assert.StartsWith("DELETE", autosCommands[0].CommandText.Trim(), StringComparison.OrdinalIgnoreCase);
            Assert.True(autosCommands[1].CommandText.TrimStart().StartsWith("INSERT INTO [dbo].[Inf_TermoConstatacao_AutosInfracao]", StringComparison.OrdinalIgnoreCase));
        }

        private static TermoConstatacao CreateBaseEntity()
        {
            return new TermoConstatacao
            {
                Id = 1,
                NumeroTermoConstatacao = "CP2024.000001",
                NumeroTermoConstatacaoTalonario = "0001",
                NomeCondutor = "Condutor",
                CpfCondutor = "00000000000",
                RgCondutor = "1234567",
                CnhCondutor = "1234567890",
                TelefoneCondutor = "11999999999",
                CepCondutor = "12345678",
                EnderecoCondutor = "Rua A",
                MunicipioUfCondutor = "Cidade/UF",
                PlacaVeiculo = "ABC1D23",
                PaisVeiculo = "BR",
                MunicipioVeiculo = "Cidade",
                UfVeiculo = "SP",
                RenavamVeiculo = "00000000000",
                MarcaModeloVeiculo = "Modelo",
                EspecieVeiculo = "Especie",
                CategoriaVeiculo = "Categoria",
                CorVeiculo = "Azul",
                CepLocalInfracao = "12345678",
                EnderecoLocalInfracao = "Rua B",
                MunicipioUfLocalInfracao = "Cidade/UF",
                DataHoraLocalInfracao = DateTime.UtcNow,
                LatitudeLocalInfracao = "-23.0",
                LongitudeLocalInfracao = "-46.0",
                Observacoes = "Observacao",
                MatriculaAgente = "12345678",
                MatriculaTestemunha1 = "12345678901",
                MatriculaTestemunha2 = "10987654321",
                DataHoraInclusao = DateTime.UtcNow,
                UsuarioInclusao = "tester",
                NumeroCondutor = "123",
                BairroCondutor = "Centro",
                NumeroLocalInfracao = "456",
                BairroLocalInfracao = "Bairro",
                CondicaoCondutor = 1,
                SubstanciaIdentificada = 1,
                TestesOferecidos = 1,
                Chassi = "9BWZZZ377VT004251",
                VeiculoEmplacado = "S",
                Observacao = "Observacao",
                RelatosCondutor = new List<RelatoCondutor>(),
                AvaliacaoCondutor = new List<AvaliacaoCondutor>(),
                AutosInfracao = new List<AutoInfracao>()
            };
        }
    }
}
