using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Application
{
    public class CampanhaApplicationService : ICampanhasTalonarioService
    {
        #region Private Fields

        private readonly ICampanhasTalonarioRepository _campanhasTalonarioRepository;

        #endregion Private Fields

        #region Public Constructors

        public CampanhaApplicationService(ICampanhasTalonarioRepository campanhasTalonarioRepository)
        {
            _campanhasTalonarioRepository = campanhasTalonarioRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> Adicionar(InfCampanhasTalonario campanha)
        {
            if (string.IsNullOrWhiteSpace(campanha.Titulo))
                throw new System.Exception("O título da campanha é obrigatório.");

            if (campanha.DataInicio == null || campanha.DataFim == null)
                throw new System.Exception("As datas de início e fim são obrigatórias.");

            return await _campanhasTalonarioRepository.Adicionar(campanha);
        }

        public async Task<IEnumerable<InfCampanhasTalonario>> ObterAtivo()
        {
            return await _campanhasTalonarioRepository.ObterAtivo();
        }

        public async Task<IEnumerable<InfCampanhasTalonario>> ObterInativas()
        {
            return await _campanhasTalonarioRepository.ObterInativas();
        }

        #endregion Public Methods
    }
}