using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Extensions;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class VeiculoViewModelMapper
    {
        #region Public Methods

        public static LocalidadeViewModel CidadeEstadoPaisMapper(CidadePaisEntity cidadeEntity, string estado, string pais)
        {
            return new LocalidadeViewModel(
                cidadeEntity.CodLocal,
                cidadeEntity.NomeLocal,
                estado,
                cidadeEntity.UF,
                pais
            );
        }

        public static CidadeViewModel CidadeMapper(CidadePaisEntity cidadeEntity)
        {
            return new CidadeViewModel(
                cidadeEntity.idCidadePais,
                cidadeEntity.CodLocal,
                cidadeEntity.TipoLocal,
                cidadeEntity.NomeLocal,
                cidadeEntity.UF
            );
        }

        public static CorViewModel CorMapper(CorEntity corEntity)
        {
            return new CorViewModel(
                corEntity.idCor,
                corEntity.Descricao,
                corEntity.DataInclusao
            );
        }

        public static EspecieViewModel EspecieMapper(EspecieEntity especieEntity)
        {
            return new EspecieViewModel(
                especieEntity.IdEspecie,
                especieEntity.Descricao,
                especieEntity.DescricaoAbreviada,
                especieEntity.DataInclusao
            );
        }

        public static MarcaModeloViewModel MarcaModeloMapper(MarcaEntity marcaEntity)
        {
            return new MarcaModeloViewModel(
                marcaEntity.IdMarca,
                marcaEntity.Descricao.Trim(),
                marcaEntity.idEspecie,
                marcaEntity.DataInclusao
            );
        }

        public static MarcaModeloTipoViewModel MarcasModeloTipoMapper(MarcaModeloTipoEntity marcaModeloTipoEntity)
        {
            return new MarcaModeloTipoViewModel(
                marcaModeloTipoEntity.IdMarca,
                marcaModeloTipoEntity.IdEspecie,
                marcaModeloTipoEntity.MarcaModelo.Trim(),
                marcaModeloTipoEntity.Especie,
                marcaModeloTipoEntity.TipoVeiculo,
                marcaModeloTipoEntity.RestricaoFazendaria,
                marcaModeloTipoEntity.Porte,
                marcaModeloTipoEntity.TemPlacaDianteira,
                marcaModeloTipoEntity.PlacaPequena,
                marcaModeloTipoEntity.PodeSerTaxi,
                marcaModeloTipoEntity.PodeSerAmbulancia,
                marcaModeloTipoEntity.PodeSerEscolar,
                marcaModeloTipoEntity.DataInclusao
            );
        }

        public static PaisViewModel PaisMapper(CidadePaisEntity paisEntity)
        {
            return new PaisViewModel(
                paisEntity.idCidadePais,
                paisEntity.TipoLocal,
                paisEntity.NomeLocal
            );
        }

        public static ProprietarioViewModel ProprietarioMapper(ProprietarioEntity proprietarioEntity)
        {
            return new ProprietarioViewModel()
            {
                Nome = proprietarioEntity.Nome,
                CPF = proprietarioEntity.CPF,
                DataNascimento = proprietarioEntity.DataNascimento,
                Sexo = proprietarioEntity.Sexo == "M" ? "Masculino" : "Feminino",
                NomeMae = proprietarioEntity.NomeMae,
                NomePai = proprietarioEntity.NomePai,
                NumeroRegistro = proprietarioEntity.NumeroRegistro,
                CategoriaCNH = proprietarioEntity.CategoriaCNH,
                DataValidadeCNH = proprietarioEntity.DataValidadeCNH,
                UFHabilitacao = proprietarioEntity.UFHabilitacao
            };
        }

        public static VeiculoAbordadoViewModel VeiculoAbordadoMapper(VeiculoAbordadoEntity veiculoAbordadoEntity)
        {
            return new VeiculoAbordadoViewModel(
                veiculoAbordadoEntity.Id,
                veiculoAbordadoEntity.Placa,
                veiculoAbordadoEntity.JSON
            );
        }

        public static VeiculoViewModel VeiculoConsultaMapper(VeiculoEntity veiculoEntity)
        {
            Mensagem mensagem = new()
            {
                Tipo = veiculoEntity.DefineTipoDeMensagem(),
                Conteudo = veiculoEntity.GeraMensagem()
            };

            return new VeiculoViewModel
            {
                Placa = veiculoEntity.Placa,
                MarcaModelo = veiculoEntity.MarcaModelo,
                Chassi = veiculoEntity.Chassi,
                Categoria = veiculoEntity.Categoria,
                Cor = veiculoEntity.Cor,
                EstadoEmplacamento = veiculoEntity.EstadoEmplacamento,
                MunicipioEmplacamento = veiculoEntity.MunicipioEmplacamento,
                PaisDoVeiculo = veiculoEntity.PaisDoVeiculo,
                Mensagem = mensagem,
                Especie = veiculoEntity.Especie,
                TipoVeiculo = veiculoEntity.TipoVeiculo,
            };
        }

        #endregion Public Methods
    }
}