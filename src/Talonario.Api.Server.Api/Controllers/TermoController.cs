using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.ViewModels;

namespace ApiTalonario.Api.Controllers
{
    /// <summary>
    /// TermoController
    /// </summary>
    [ApiController]
    [Route("api_v4/termo-constatacao")]
    public class TermoController : ControllerBase
    {
        private readonly TermoService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public TermoController(TermoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cadastro do Termo de Constatação
        /// </summary>
        /// <param name="termoInput"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("cadastrar-termo-constatacao")]
        public IActionResult CadastrarTermoConstatacao([FromBody] TermoConstatacaoInputDto termoInput)
        {
            try
            {
                var resultado = _service.CadastrarTermoFromMobile(termoInput);

                return StatusCode(StatusCodes.Status201Created, new
                {
                    success = true,
                    message = "Termo cadastrado com sucesso",
                    data = new { id = resultado.NumeroTermoConstatacao }
                });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[WARN] Validação falhou ao cadastrar termo: {ex}");
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Erro ao cadastrar termo: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "Erro interno ao processar a requisição" });
            }
        }

        /// <summary>
        /// Consulta do Termo de Constatação
        /// </summary>
        /// <param name="numeroTc"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("consultar-termo-constatacao/{numeroTc}")]
        public IActionResult ConsultarTermoConstatacao(string numeroTc)
        {
            try
            {
                var termo = _service.ConsultarTermo(numeroTc);
                return Ok(termo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Listagem dos Termos de Constatação
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("listar-termo-constatacao")]
        public IActionResult ListarTermoConstatacao()
        {
            try
            {
                var termos = _service.ListarTermos();
                return Ok(termos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Pesquisa dos Termos de Constatação
        /// </summary>
        /// <param name="pesquisa"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("pesquisa-termo-constatacao/{pesquisa}")]
        public IActionResult PesquisaTermoConstatacao(string pesquisa)
        {
            try
            {
                var termos = _service.PesquisarTermos(pesquisa);
                return Ok(termos);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Assina Termo de Constatação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("assinar-termo-constatacao")]
        public IActionResult AssinarTermoConstatacao([FromBody] AssinaturaTermoRequest request)
        {
            try
            {
                var sucesso = _service.AssinarTermo(request.NumeroTc, request.Matricula);
                return Ok(new { Sucesso = sucesso });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Adiciona Observação ao Termo de Constestação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("observacao-termo-constatacao")]
        public IActionResult ObservacaoTermoConstatacao([FromBody] ObservacaoTermoRequest request)
        {
            try
            {
                var sucesso = _service.AdicionarObservacao(request.NumeroTc, request.Observacao);
                return Ok(new { Sucesso = sucesso });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Edição do Termo de Constatação
        /// </summary>
        /// <param name="termo"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("editar-termo-constatacao")]
        public IActionResult EditarTermoConstatacao([FromBody] TermoConstatacao termo)
        {
            try
            {
                var resultado = _service.EditarTermo(termo);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Exclusão do Termo de Constatação
        /// </summary>
        /// <param name="numeroTc"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("excluir-termo-constatacao/{numeroTc}")]
        public IActionResult ExcluirTermoConstatacao(string numeroTc)
        {
            try
            {
                var sucesso = _service.ExcluirTermo(numeroTc);
                return Ok(new { Sucesso = sucesso });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cancelamento do Termo de Constatação
        /// </summary>
        /// <param name="numeroTc"></param>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("cancelar-termo-constatacao/{numeroTc}/{matricula}")]
        public IActionResult CancelarTermoConstatacao(string numeroTc, string matricula)
        {
            try
            {
                var sucesso = _service.CancelarTermo(numeroTc, matricula);
                return Ok(new { Sucesso = sucesso });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Adição do Comprovante do Termo de Constatação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("upload-comprovante-termo-constatacao")]
        public IActionResult UploadComprovanteTermoConstatacao([FromBody] ComprovanteTermoRequest request)
        {
            try
            {
                var resultado = _service.UploadComprovante(request.IdTermo, request.Comprovante);
                return Ok(new { ComprovanteId = resultado });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remoção do Comprovante do Termo de Constatação
        /// </summary>
        /// <param name="idTermo"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("delete-comprovante-termo-constatacao/{idTermo}")]
        public IActionResult DeleteComprovanteTermoConstatacao(int idTermo)
        {
            try
            {
                var sucesso = _service.RemoverComprovante(idTermo);
                return Ok(new { Sucesso = sucesso });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Obtenção do Termo de Constatação
        /// </summary>
        /// <param name="idTermo"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("obter-comprovante-termo-constatacao/{idTermo}")]
        public IActionResult ObterComprovanteTermoConstatacao(int idTermo)
        {
            try
            {
                var comprovante = _service.ObterComprovante(idTermo);
                return Ok(comprovante);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }
    }
}