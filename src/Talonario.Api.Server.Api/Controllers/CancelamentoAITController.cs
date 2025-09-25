using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Controllers
{
    /// <summary>
    /// CancelamentoAITController
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api_v4/solicitacoes-cancelamento")]
    public class CancelamentoAITController : ControllerBase
    {
        private readonly ICancelamentoAITService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public CancelamentoAITController(ICancelamentoAITService service)
        {
            _service = service;
        }

        /// <summary>
        /// Solicitação de Cancelamento
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] SolicitacaoCancelamentoAITViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _service.RegistrarSolicitacaoAsync(viewModel);
                return CreatedAtAction(nameof(Post), new { id });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno ao processar solicitação");
            }
        }
    }
}