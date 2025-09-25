using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class PessoaViewModelMapper
    {
        #region Public Methods

        public static PessoaAbordadaViewModel PessoaAbordadaMapper(PessoaAbordadaEntity pessoaAbordadaEntity)
        {
            return new PessoaAbordadaViewModel(
                pessoaAbordadaEntity.Id,
                pessoaAbordadaEntity.IdPessoa,
                pessoaAbordadaEntity.JSON
            );
        }

        public static PessoaViewModel PessoaMapper(PessoaEntity pessoaEntity)
        {
            return new PessoaViewModel
            {
                Id = pessoaEntity.Id,
                CPF = pessoaEntity.CPF,
                DataNascimento = pessoaEntity.DataNascimento,
                Habilitacao = pessoaEntity.Habilitacao,
                Nome = pessoaEntity.Nome,
                NomeMae = pessoaEntity.NomeMae,
                NomePai = pessoaEntity.NomePai,
                Renach = pessoaEntity.Renach,
                Sexo = pessoaEntity.Sexo,
                UFHabilitacao = pessoaEntity.UFHabilitacao
            };
        }

        #endregion Public Methods
    }
}