using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// PessoaController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class PessoaController : ControllerBase
    {
        #region Private Fields

        private readonly IPessoaApplicationService _pessoaApplicationService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="pessoaApplicationService"></param>
        public PessoaController(IPessoaApplicationService pessoaApplicationService)
        {
            this._pessoaApplicationService = pessoaApplicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Cadastrar Pessoa Abordada
        /// </summary>
        /// <param name="pessoaAbordada">Pessoa Abordada</param>
        /// <returns>Id da Pessoa Abordada inserida</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("PessoaAbordada")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirPessoaAbordada(PessoaAbordadaViewModel pessoaAbordada)
        {
            try
            {
                var result = await _pessoaApplicationService.InserirPessoaAbordada(pessoaAbordada);

                if (result == 0)
                    return BadRequest("Nenhum registro foi inserido");

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consultar Pessoa Abordada por IdPessoa
        /// </summary>
        /// <param name="idPessoa">IdPessoa da Pessoa Abordada</param>
        /// <returns>Pessoa Abordada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("PessoaAbordada/{idPessoa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoAbordadoViewModel>> ObterPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            try
            {
                var result = await _pessoaApplicationService.ObterPessoaAbordadaPorIdPessoa(idPessoa);

                if (result == null)
                    return NotFound("Pessoa Abordada não encontrada");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consultar Pessoas Abordadas
        /// </summary>
        /// <returns>Lista de Pessoas Abordadas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("PessoasAbordadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VeiculoAbordadoViewModel>>> ObterPessoasAbordadas()
        {
            try
            {
                var result = await _pessoaApplicationService.ObterPessoasAbordadas();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Pesquisa pessoa por CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("Pessoa/CPF/{cpf}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PessoaViewModel>> PesquisarPorCPF(string cpf)
        {
            try
            {
                PessoaViewModel pessoa = await _pessoaApplicationService.PesquisarPorCPF(cpf);

                if (pessoa is not null)
                {
                    return Ok(pessoa);
                }

                pessoa = await _pessoaApplicationService.PesquisarExternaPorCPF(cpf);

                if (pessoa is null)
                    return NotFound("CPF não encontrado");

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Remover Pessoa Abordada por IdPessoa
        /// </summary>
        /// <param name="idPessoa">IdPessoa da Pessoa Abordada</param>
        /// <returns></returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("PessoaAbordada/{idPessoa}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> RemoverPessoaAbordadaPorIdPessoa(string idPessoa)
        {
            try
            {
                var result = await _pessoaApplicationService.RemoverPessoaAbordadaPorIdPessoa(idPessoa);

                if (!result)
                    return NotFound("Nenhum registro foi removido");

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion Public Methods
    }
}