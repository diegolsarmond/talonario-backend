using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// UsuarioController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class UsuarioController : ControllerBase
    {
        #region Private Fields

        private readonly IUsuarioApplicationService _usuarioApplicationService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="usuarioApplicationService"></param>
        public UsuarioController(IUsuarioApplicationService usuarioApplicationService)
        {
            this._usuarioApplicationService = usuarioApplicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Login por CPF e Senha
        /// </summary>
        /// <param name="usuarioLoginInput">Dados do Login</param>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> Login(UsuarioLoginInputModel usuarioLoginInput)
        {
            try
            {
                var result = await _usuarioApplicationService.LoginPorCredenciais(usuarioLoginInput);

                if (result == null) return NotFound("Usuário e/ou Senha inválidos");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Logout por CPF e Dispositivo
        /// </summary>
        /// <param name="usuarioLogout">Dados do Logout</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("Logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult> Logout(UsuarioLogout usuarioLogout)
        {
            try
            {
                var fezLogout = await _usuarioApplicationService.Logout(usuarioLogout);

                if (fezLogout)
                    return NoContent();

                return BadRequest("Ocorreu um erro ao fazer o logout");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Consulta de Usuários do Talonário
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> ObterTodos()
        {
            try
            {
                var result = await _usuarioApplicationService.ObterTodos();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Retorna flag que avisa se matrícula pode assinar
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [HttpGet("PodeAssinar/{matricula}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> PodeAssinar([FromRoute] string matricula)
        {
            try
            {
                bool matriculaValida = int.TryParse(matricula, out int _matricula);

                if (!matriculaValida)
                    return BadRequest("Matricula inválida");

                var result = await _usuarioApplicationService.PodeAssinar(matricula);

                if (result)
                    return NoContent();

                return NotFound("Matrícula não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion Public Methods
    }
}