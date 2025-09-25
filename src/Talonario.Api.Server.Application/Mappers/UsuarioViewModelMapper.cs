using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class UsuarioViewModelMapper
    {
        #region Public Methods

        public static UsuarioViewModel UsuarioMapper(UsuarioEntity usuarioEntity)
        {
            return new UsuarioViewModel(
                usuarioEntity.Id,
                usuarioEntity.Nome,
                usuarioEntity.CPF,
                usuarioEntity.Email,
                usuarioEntity.Permissoes,
                usuarioEntity.Empresa,
                usuarioEntity.IdEmpresa,
                usuarioEntity.Competencia,
                usuarioEntity.Token,
                usuarioEntity.Senha,
                usuarioEntity.IdTalonarioDispositivo,
                usuarioEntity.IdDispositivo,
                usuarioEntity.Sequencia,
                usuarioEntity.Ativo,
                usuarioEntity.Matricula,
                usuarioEntity.MatriculaAgente
            );
        }

        #endregion Public Methods
    }
}