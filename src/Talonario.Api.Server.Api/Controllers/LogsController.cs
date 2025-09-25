using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Controllers
{
    /// <summary>
    /// LogsController
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("talonario/logs")]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public LogsController(ILogsService service)
        {
            _service = service;
        }

        /// <summary>
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        [HttpPost("lote")]
        public async Task<IActionResult> PostLote([FromBody] List<RegistroLogTalonarioViewModel> logs)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.RegistrarLogsAsync(logs);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao registrar logs", detalhe = ex.Message });
            }
        }
    }
}