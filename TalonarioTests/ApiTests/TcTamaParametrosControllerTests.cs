using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Talonario.Api.Server.Api.Controllers;
using Talonario.Api.Server.Application.Interfaces.Services;
using Xunit;

namespace TalonarioTests.ApiTests
{
    public class TcTamaParametrosControllerTests
    {
        private readonly Mock<ITcTamaParametrosService> _mockService;
        private readonly TcTamaParametrosController _controller;

        public TcTamaParametrosControllerTests()
        {
            _mockService = new Mock<ITcTamaParametrosService>();
            _controller = new TcTamaParametrosController(_mockService.Object);
        }

        [Fact]
        public async Task ObterTodos_RetornaOk()
        {
            // Arrange

            var resultadoEsperado = new { };
            _mockService.Setup(service => service.ObterTodosParametros()).ReturnsAsync(resultadoEsperado);

            // Act
            var resuolt = await _controller.ObterTodos();

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(resuolt);
            Assert.Equal(resultadoEsperado, okResult.Value);
        }

        [Fact]
        public async Task ObterTodos_RetornaInternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.ObterTodosParametros()).ThrowsAsync(new Exception("Erro simulado"));

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}