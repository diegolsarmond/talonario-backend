using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Controllers;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.ApiTests
{
    public class UsuarioControllerTests
    {
        #region Public Methods

        [Fact]
        public async Task Logout_ComDadosCorretos_RetornaNoContent()
        {
            //arrange
            Mock<IUsuarioApplicationService> usuarioService = new();
            usuarioService.Setup(u => u.Logout(It.IsAny<UsuarioLogout>()))
                          .ReturnsAsync(true);
            UsuarioController usuarioController = new(usuarioService.Object);
            UsuarioLogout usuarioLogout = new("cpf", "idDispositivo");

            //act
            var result = await usuarioController.Logout(usuarioLogout);

            //assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Logout_ComDadosIncorretos_RetornaBadRequest()
        {
            //arrange
            Mock<IUsuarioApplicationService> usuarioService = new();
            usuarioService.Setup(u => u.Logout(It.IsAny<UsuarioLogout>()))
                          .ReturnsAsync(false);
            UsuarioController usuarioController = new(usuarioService.Object);
            UsuarioLogout usuarioLogout = new("cpf", "idDispositivo");

            //act
            var result = await usuarioController.Logout(usuarioLogout);

            //assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Logout_LancaException_RetornaBadRequest()
        {
            //arrange
            const string MENSAGEM_EXCEPTION = "Ocorreu o erro xpto";
            Mock<IUsuarioApplicationService> usuarioService = new();
            usuarioService.Setup(u => u.Logout(It.IsAny<UsuarioLogout>()))
                          .ThrowsAsync(new Exception(MENSAGEM_EXCEPTION));
            UsuarioController usuarioController = new(usuarioService.Object);
            UsuarioLogout usuarioLogout = new("cpf", "idDispositivo");

            //act
            var result = await usuarioController.Logout(usuarioLogout) as BadRequestObjectResult;

            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(MENSAGEM_EXCEPTION, result.Value);
        }

        #endregion Public Methods
    }
}