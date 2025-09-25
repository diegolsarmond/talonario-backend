using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;

namespace Talonario.Api.Server.Api.Controllers
{
    /// <summary>
    /// Controlador para obter os parâmetros TcTama.
    /// </summary>
    [Route("api_v4/[controller]")]
    [ApiController]
    [Authorize]
    public class TcTamaParametrosController : ControllerBase
    {
        private readonly ITcTamaParametrosService _service;

        /// <summary>
        /// Construtor do controlador TcTamaParametrosController.
        /// </summary>
        /// <param name="service">Serviço de TcTamaParametros.</param>
        public TcTamaParametrosController(ITcTamaParametrosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém todos os parâmetros TcTama ativos.
        /// </summary>
        /// <returns>Lista de parâmetros organizados em um JSON.</returns>

        [HttpGet("obter-todos")]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var resultado = await _service.ObterTodosParametros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao obter os parâmetros.", detalhe = ex.Message });
            }
        }
    }
}