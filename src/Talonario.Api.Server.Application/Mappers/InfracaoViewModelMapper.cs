using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class InfracaoViewModelMapper
    {
        #region Public Methods

        public static TipoInfracaoViewModel TipoInfracaoMapper(TipoInfracaoEntity tipoInfracaoEntity)
        {
            return new TipoInfracaoViewModel(
                tipoInfracaoEntity.IdTipoInfracao,
                tipoInfracaoEntity.CodigoInfracao,
                tipoInfracaoEntity.Desdobramento,
                tipoInfracaoEntity.Competencia,
                tipoInfracaoEntity.Artigo,
                tipoInfracaoEntity.Infrator,
                tipoInfracaoEntity.Pontos,
                tipoInfracaoEntity.Valor,
                tipoInfracaoEntity.Natureza,
                tipoInfracaoEntity.DescricaoResumida,
                tipoInfracaoEntity.DescricaoCompleta,
                tipoInfracaoEntity.Equipamento,
                tipoInfracaoEntity.CodigoEquipamento,
                tipoInfracaoEntity.RequerAnexo,
                tipoInfracaoEntity.RequerEquipamento,
                tipoInfracaoEntity.RetemVeiculo,
                tipoInfracaoEntity.ApresentaCondutor,
                tipoInfracaoEntity.ApreensaoPlaca,
                tipoInfracaoEntity.TransbordoCarga,
                tipoInfracaoEntity.ApreensaoVeiculo,
                tipoInfracaoEntity.SuspensaoCarteira,
                tipoInfracaoEntity.DataIniVigencia,
                tipoInfracaoEntity.DataFimVigencia,
                tipoInfracaoEntity.DataInclusao,
                tipoInfracaoEntity.Ativo
            );
        }

        #endregion Public Methods
    }
}