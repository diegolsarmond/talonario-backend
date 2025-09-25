using System;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Extensions;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class CondutorApplicationService : ICondutorApplicationService
    {
        #region Private Fields

        private readonly ICondutorRepository _condutorRepository;

        #endregion Private Fields

        #region Public Constructors

        public CondutorApplicationService(ICondutorRepository condutorRepository)
        {
            _condutorRepository = condutorRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CondutorViewModel> PesquisarExternaPorCpf(string cpf)
        {
            string _cpf = cpf.RemoveMask();

            if (!_cpf.Has11DigitsWithoutMask())
                throw new ArgumentException("CPF deve ter 11 dígitos");

            if (!_cpf.IsValidCpf())
                throw new ArgumentException("CPF inválido");

            CondutorEntity condutorEntity = await _condutorRepository.PesquisarExternaPorCPF(_cpf);

            if (condutorEntity is not null)
            {
                return CondutorViewModelMapper.CondutorMapper(condutorEntity);
            }

            return null;
        }

        public async Task<CondutorViewModel> PesquisarPorCpf(string cpf)
        {
            var result = await _condutorRepository.PesquisarPorCpf(cpf);

            if (result is null) return null;

            var viewModel = CondutorViewModelMapper.CondutorMapper(result);
            return viewModel;
        }

        #endregion Public Methods
    }
}