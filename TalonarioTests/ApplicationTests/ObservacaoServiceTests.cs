using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talonario.Api.Server.Application;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Xunit;

namespace TalonarioTests.ApplicationTests
{
    public class ObservacaoServiceTests
    {
        #region Public Methods

        [Fact]
        public async Task GetAllAtivos_RepositorioRetornandoLista_RetornaLista()
        {
            //arrange
            Mock<IObservacaoRepository> observacaoRepository = new();
            observacaoRepository.Setup(o => o.GetAllAtivos())
                                .ReturnsAsync(() => new List<ObservacaoEntity>()
                                {
                                    new(1,"titulo1","descricao1"),
                                    new(2,"titulo2","descricao2")
                                });

            ObservacaoService observacaoService = new(observacaoRepository.Object);

            //act
            var lista = await observacaoService.GetAllAtivos();

            //assert
            Assert.NotNull(lista);
            Assert.NotEmpty(lista);
            Assert.Equal(2, lista.Count());
            Assert.Equal(1, lista.First().Id);
            Assert.Equal("titulo1", lista.First().Titulo);
            Assert.Equal("descricao1", lista.First().Descricao);
        }

        [Fact]
        public async Task GetAllAtivos_RepositorioRetornandoNull_RetornaListaVazia()
        {
            //arrange
            Mock<IObservacaoRepository> observacaoRepository = new();
            observacaoRepository.Setup(o => o.GetAllAtivos())
                                .ReturnsAsync(() => null);

            ObservacaoService observacaoService = new(observacaoRepository.Object);

            //act
            var lista = await observacaoService.GetAllAtivos();

            //assert
            Assert.NotNull(lista);
            Assert.Empty(lista);
        }

        #endregion Public Methods
    }
}