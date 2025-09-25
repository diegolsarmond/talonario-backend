using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Api.Controllers;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.ApiTests
{
    public class ObservacaoControllerTests
    {
        #region Public Methods

        [Fact]
        public async Task Get_ComServiceLancandoExcecao_RetornaBadRequest()
        {
            //arrange
            string MENSAGEM_RETORNO = "Simulação de exception";
            Mock<IObservacaoService> service = new();
            service.Setup(s => s.GetAllAtivos())
                   .ThrowsAsync(new Exception(MENSAGEM_RETORNO));

            ObservacaoController observacaoController = new(service.Object);

            //act
            var retorno = await observacaoController.Get() as BadRequestObjectResult;
            string? mensagem = retorno?.Value?.ToString();

            //assert
            Assert.IsType<BadRequestObjectResult>(retorno);
            Assert.Equal(MENSAGEM_RETORNO, mensagem);
        }

        [Fact]
        public async Task Get_ComServiceRetornandoLista_RetornaOk()
        {
            //arrange
            Mock<IObservacaoService> service = new();
            service.Setup(s => s.GetAllAtivos())
                   .ReturnsAsync(() => new List<ObservacaoViewModel>() {
                       new(1,"titulo1","descricao1"),
                       new(2,"titulo2","descricao2")
                   });

            ObservacaoController observacaoController = new(service.Object);

            //act
            OkObjectResult? retorno = await observacaoController.Get() as OkObjectResult;
            List<ObservacaoViewModel>? lista = retorno?.Value as List<ObservacaoViewModel>;

            //assert
            Assert.IsType<OkObjectResult>(retorno);
            Assert.NotNull(lista);
            Assert.Equal(2, lista.Count);
        }

        [Fact]
        public async Task Get_ComServiceRetornandoListaVazia_RetornaOk()
        {
            //arrange
            Mock<IObservacaoService> service = new();
            service.Setup(s => s.GetAllAtivos())
                   .ReturnsAsync(() => new List<ObservacaoViewModel>() { });

            ObservacaoController observacaoController = new(service.Object);

            //act
            OkObjectResult? retorno = await observacaoController.Get() as OkObjectResult;
            List<ObservacaoViewModel>? lista = retorno?.Value as List<ObservacaoViewModel>;

            //assert
            Assert.IsType<OkObjectResult>(retorno);
            Assert.NotNull(lista);
            Assert.Empty(lista);
        }

        #endregion Public Methods
    }
}