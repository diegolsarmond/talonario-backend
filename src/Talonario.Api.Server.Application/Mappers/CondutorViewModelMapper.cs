using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class CondutorViewModelMapper
    {
        #region Public Methods

        public static CondutorViewModel CondutorMapper(CondutorEntity condutorEntity)
        {
            return new()
            {
                Nome = condutorEntity.Nome,
                CPF = condutorEntity.CPF,
                DataNascimento = condutorEntity.DataNascimento,
                Sexo = condutorEntity.Sexo == "1" ? "Masculino" : "Feminino",
                NomeMae = condutorEntity.NomeMae,
                NomePai = condutorEntity.NomePai,
                NumeroRegistro = condutorEntity.NumeroRegistro,
                CategoriaCNH = condutorEntity.CategoriaCNH,
                DataValidadeCNH = condutorEntity.DataValidadeCNH,
                UFHabilitacao = condutorEntity.UFHabilitacao
            };
        }

        #endregion Public Methods
    }
}