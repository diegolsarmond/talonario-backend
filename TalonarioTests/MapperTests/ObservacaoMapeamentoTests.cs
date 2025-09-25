using System.Collections.Generic;
using System.Linq;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.MapperTests
{
    public class ObservacaoMapeamentoTests
    {
        #region Public Methods

        [Fact]
        public void ObservacaoMapper_ComDadosValidos_RetornaViewModel()
        {
            //arrange
            ObservacaoEntity observacaoEntity = new(1, "titulo", "descricao");

            //act
            ObservacaoViewModel observacaoViewModel = ObservacaoViewModelMapper.ObservacaoMapper(observacaoEntity);

            //assert
            Assert.Equal(observacaoEntity.Id, observacaoViewModel.Id);
            Assert.Equal(observacaoEntity.Titulo, observacaoViewModel.Titulo);
            Assert.Equal(observacaoEntity.Descricao, observacaoViewModel.Descricao);
        }

        [Fact]
        public void ObservacaoMapper_ComListaDadosValidos_RetornaListaViewModel()
        {
            //arrange
            List<ObservacaoEntity> observacoesEntity = new()
            {
                new(1, "titulo1", "descricao1"),
                new(2, "titulo2", "descricao2")
            };

            //act
            var listaObservacaoViewModel = ObservacaoViewModelMapper.ObservacaoMapper(observacoesEntity);

            //assert
            Assert.NotNull(listaObservacaoViewModel);
            Assert.Equal(2, listaObservacaoViewModel.Count());
            Assert.Equal(observacoesEntity.First().Id, listaObservacaoViewModel.First().Id);
            Assert.Equal(observacoesEntity.First().Titulo, listaObservacaoViewModel.First().Titulo);
            Assert.Equal(observacoesEntity.First().Descricao, listaObservacaoViewModel.First().Descricao);
        }

        [Fact]
        public void ObservacaoMapper_RecebendoNull_RetornaNull()
        {
            //arrange
            List<ObservacaoEntity> observacoesEntity = null!;

            //act
            var listaObservacaoViewModel = ObservacaoViewModelMapper.ObservacaoMapper(observacoesEntity);

            //assert
            Assert.Null(listaObservacaoViewModel);
        }

        #endregion Public Methods
    }
}