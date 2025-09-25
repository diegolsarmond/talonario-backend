using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Enums;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;
using Xunit;

namespace TalonarioTests.MapperTests
{
    public class VeiculoMapeamentoTest
    {
        #region Public Methods

        [Fact]
        public void VeiculoConsultaMapper_ComRouboOuFurto_DefineMensagemCorreta()
        {
            //arrange
            VeiculoEntity veiculoEntity = new()
            {
                Categoria = "Categoria",
                Chassi = "123abc",
                Cor = "Cor",
                EstadoEmplacamento = "RO",
                FurtadoOuRoubado = true,
                MarcaModelo = "Marca Modelo",
                MunicipioEmplacamento = "Munic�pio",
                PaisDoVeiculo = "Brasil",
                Placa = "Placa",
                TotalAutuacoes = 0,
                TotalMultas = 0
            };

            //act
            VeiculoViewModel veiculoViewModel = VeiculoViewModelMapper.VeiculoConsultaMapper(veiculoEntity);

            //assert
            Assert.Equal(TipoMensagem.FurtoOuRoubo, veiculoViewModel.Mensagem.Tipo);
            Assert.Equal("Ve�culo com registro de furto e/ou roubo", veiculoViewModel.Mensagem.Conteudo);
        }

        [Fact]
        public void VeiculoConsultaMapper_ComRouboOuFurtoEComAutuacoesEMultas_DefineMensagemCorreta()
        {
            //arrange
            VeiculoEntity veiculoEntity = new()
            {
                Categoria = "Categoria",
                Chassi = "123abc",
                Cor = "Cor",
                EstadoEmplacamento = "RO",
                FurtadoOuRoubado = true,
                MarcaModelo = "Marca Modelo",
                MunicipioEmplacamento = "Munic�pio",
                PaisDoVeiculo = "Brasil",
                Placa = "Placa",
                TotalMultas = 2,
                TotalAutuacoes = 5
            };

            //act
            VeiculoViewModel veiculoViewModel = VeiculoViewModelMapper.VeiculoConsultaMapper(veiculoEntity);

            //assert
            Assert.Equal(TipoMensagem.FurtoOuRoubo, veiculoViewModel.Mensagem.Tipo);
            Assert.Contains("Ve�culo com registro de furto e/ou roubo", veiculoViewModel.Mensagem.Conteudo);
            Assert.Contains("com 5 autua��o(�es)", veiculoViewModel.Mensagem.Conteudo);
            Assert.Contains("e 2 multa(s)", veiculoViewModel.Mensagem.Conteudo);
            Assert.DoesNotContain("\n", veiculoViewModel.Mensagem.Conteudo);
            Assert.DoesNotContain("\t", veiculoViewModel.Mensagem.Conteudo);
        }

        [Fact]
        public void VeiculoConsultaMapper_SemRouboOuFurtoEComAutuacoesEMultas_DefineMensagemCorreta()
        {
            //arrange
            VeiculoEntity veiculoEntity = new()
            {
                Categoria = "Categoria",
                Chassi = "123abc",
                Cor = "Cor",
                EstadoEmplacamento = "RO",
                FurtadoOuRoubado = false,
                MarcaModelo = "Marca Modelo",
                MunicipioEmplacamento = "Munic�pio",
                PaisDoVeiculo = "Brasil",
                Placa = "Placa",
                TotalMultas = 2,
                TotalAutuacoes = 5
            };

            //act
            VeiculoViewModel veiculoViewModel = VeiculoViewModelMapper.VeiculoConsultaMapper(veiculoEntity);

            //assert
            Assert.Equal(TipoMensagem.AutuacaoOuMulta, veiculoViewModel.Mensagem.Tipo);
            Assert.DoesNotContain("Ve�culo com registro de furto e/ou roubo", veiculoViewModel.Mensagem.Conteudo);
            Assert.Contains("Ve�culo com d�bitos tribut�rios ou impedimentos de circula��o", veiculoViewModel.Mensagem.Conteudo);
        }

        [Fact]
        public void VeiculoConsultaMapper_SemRouboOuFurtoESemAutuacoesEMultas_DefineMensagemCorreta()
        {
            //arrange
            VeiculoEntity veiculoEntity = new()
            {
                Categoria = "Categoria",
                Chassi = "123abc",
                Cor = "Cor",
                EstadoEmplacamento = "RO",
                FurtadoOuRoubado = false,
                MarcaModelo = "Marca Modelo",
                MunicipioEmplacamento = "Munic�pio",
                PaisDoVeiculo = "Brasil",
                Placa = "Placa",
                TotalMultas = 0,
                TotalAutuacoes = 0
            };

            //act
            VeiculoViewModel veiculoViewModel = VeiculoViewModelMapper.VeiculoConsultaMapper(veiculoEntity);

            //assert
            Assert.Equal(TipoMensagem.Ok, veiculoViewModel.Mensagem.Tipo);
            Assert.Contains("Este ve�culo n�o tem autua��o e n�o tem multas", veiculoViewModel.Mensagem.Conteudo);
        }

        #endregion Public Methods
    }
}