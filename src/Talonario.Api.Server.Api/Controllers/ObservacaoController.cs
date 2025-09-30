using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Api.Controllers
{
    /// <summary>
    /// ObservacaoController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class ObservacaoController : Controller
    {
        #region Private Fields

        private readonly IObservacaoService _observacaoService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="observacaoService"></param>
        public ObservacaoController(IObservacaoService observacaoService)
        {
            _observacaoService = observacaoService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Retorna todas as observações
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var observacoes = await _observacaoService.GetAllAtivos();

                if (observacoes == null || !observacoes.Any())
                    return NotFound("Não existe nenhuma operação cadastrada.");

                return Ok(observacoes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion Public Methods
    }
}