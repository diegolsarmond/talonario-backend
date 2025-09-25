using Moq;
using System;
using System.Threading.Tasks;
using Talonario.Api.Server.Application;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.ApplicationTests
{
    public class InfracaoApplicationServiceTests
    {
        [Fact]
        public async Task InserirInfracaoNaoTransmitida_AtualizaRegistroExistente_RetornaLinhasAfetadas()
        {
            // Arrange
            var infracaoRepositoryMock = new Mock<IInfracaoRepository>();
            infracaoRepositoryMock
                .Setup(repository => repository.AtualizarInfracaoNaoTransmitidaAsync(It.IsAny<InfracaoNaoTransmitidaViewModel>()))
                .ReturnsAsync(1);

            var service = new InfracaoApplicationService(infracaoRepositoryMock.Object);

            var infracaoNaoTransmitida = new InfracaoNaoTransmitidaViewModel(
                0,
                "AIT1234567",
                "{}",
                "veiculo",
                null,
                null,
                "SUCESSO",
                DateTime.UtcNow);

            // Act
            var resultado = await service.InserirInfracaoNaoTransmitida(infracaoNaoTransmitida);

            // Assert
            Assert.Equal(1, resultado);
            infracaoRepositoryMock.Verify(
                repository => repository.InserirInfracaoNaoTransmitida(It.IsAny<InfracaoNaoTransmitidaViewModel>()),
                Times.Never);
        }

        [Fact]
        public async Task InserirInfracaoNaoTransmitida_InsereQuandoRegistroInexistente_RetornaIdInserido()
        {
            // Arrange
            var infracaoRepositoryMock = new Mock<IInfracaoRepository>();
            infracaoRepositoryMock
                .Setup(repository => repository.AtualizarInfracaoNaoTransmitidaAsync(It.IsAny<InfracaoNaoTransmitidaViewModel>()))
                .ReturnsAsync(0);
            infracaoRepositoryMock
                .Setup(repository => repository.InserirInfracaoNaoTransmitida(It.IsAny<InfracaoNaoTransmitidaViewModel>()))
                .ReturnsAsync(42);

            var service = new InfracaoApplicationService(infracaoRepositoryMock.Object);

            var infracaoNaoTransmitida = new InfracaoNaoTransmitidaViewModel(
                0,
                "AIT1234567",
                "{}",
                "veiculo",
                null,
                null,
                "ERRO",
                DateTime.UtcNow);

            // Act
            var resultado = await service.InserirInfracaoNaoTransmitida(infracaoNaoTransmitida);

            // Assert
            Assert.Equal(42, resultado);
            infracaoRepositoryMock.Verify(
                repository => repository.InserirInfracaoNaoTransmitida(It.IsAny<InfracaoNaoTransmitidaViewModel>()),
                Times.Once);
        }
    }
}
