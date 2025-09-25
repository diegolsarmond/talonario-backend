using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// CampanhasController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class CampanhasController : ControllerBase
    {
        #region Private Fields

        private readonly ICampanhasTalonarioService _campanhasTalonarioService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="campanhasTalonarioService"></param>
        public CampanhasController(ICampanhasTalonarioService campanhasTalonarioService)
        {
            _campanhasTalonarioService = campanhasTalonarioService ?? throw new ArgumentNullException(nameof(campanhasTalonarioService));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Consulta ativos
        /// </summary>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<InfCampanhasTalonario>>> ObterAtivos()
        {
            var result = await _campanhasTalonarioService.ObterAtivo();
            return result is not null ? Ok(result) : NotFound("Nenhuma campanha ativa encontrada.");
        }

        /// <summary>
        /// Consulta inativos
        /// </summary>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("inativos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<InfCampanhasTalonario>>> ObterInativos()
        {
            var result = await _campanhasTalonarioService.ObterInativas();
            return result is not null ? Ok(result) : NotFound("Nenhuma campanha inativa encontrada.");
        }

        #endregion Public Methods
    }
}