using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Controllers
{
    /// <summary>
    /// TamaController
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api_v4/termo-adocao-medida-administrativa")]
    public class TamaController : ControllerBase
    {
        private readonly ITamaService _tamaService;
        private readonly ITermoAdocaoService _termoAdocaoService;
        private readonly ILogger<TamaController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tamaService"></param>
        /// <param name="termoAdocaoService"></param>
        /// <param name="logger"></param>
        public TamaController(
            ITamaService tamaService,
            ITermoAdocaoService termoAdocaoService,
            ILogger<TamaController> logger)
        {
            _tamaService = tamaService;
            _termoAdocaoService = termoAdocaoService;
            _logger = logger;
        }

        /// <summary>
        /// Cadastro do Termo de Adoção
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("cadastrar-termo-adocao-completo")]
        public async Task<IActionResult> CadastrarTermoAdocaoCompleto([FromBody] TamaInputCompletoViewModel input)
        {
            try
            {
                if (input == null)
                {
                    _logger.LogWarning("Tentativa de cadastro com input nulo");
                    return BadRequest("Dados inválidos");
                }

                var resultado = await _tamaService.CadastrarTamaAsync(input);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no cadastro do termo de adoção");
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }

        /// <summary>
        /// Obter ...
        /// </summary>
        /// <returns></returns>
        [HttpGet("dropdown")]
        public async Task<IActionResult> ObterValoresDropdown()
        {
            try
            {
                var resultado = await _termoAdocaoService.ObterItensGeraisAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dados dropdown");
                return BadRequest(new { success = false, message = "Erro ao buscar os dados" });
            }
        }
    }
}