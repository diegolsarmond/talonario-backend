using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// InfracaoController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class InfracaoController : ControllerBase
    {
        #region Private Fields

        private readonly IInfracaoApplicationService _infracaoApplicationService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="infracaoApplicationService"></param>
        public InfracaoController(IInfracaoApplicationService infracaoApplicationService)
        {
            this._infracaoApplicationService = infracaoApplicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Cadastro de Infração
        /// </summary>
        /// <param name="infracao">Infração</param>
        /// <returns>Id da Infração inserida</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("Infracao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirInfracao(InfracaoViewModel infracao)
        {
            try
            {
                var result = await _infracaoApplicationService.InserirInfracao(infracao);

                if (result != "SUCESSO")
                {
                    await _infracaoApplicationService.AtualizarMotivoProcessamento(infracao.NumeroAuto, result);
                    return BadRequest(result);
                }

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                await _infracaoApplicationService.AtualizarMotivoProcessamento(infracao.NumeroAuto, ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastro de Infração Anexos
        /// </summary>
        /// <param name="infracaoAnexos">Infração Anexos</param>
        /// <returns>Id da Infração Anexo</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("InfracaoAnexos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirInfracaoAnexo(InfracaoAnexoInputModel infracaoAnexos)
        {
            try
            {
                var result = await _infracaoApplicationService.InserirInfracaoAnexo(infracaoAnexos);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastro de Infração Não Transmitida
        /// </summary>
        /// <param name="infracaoNaoTransmitida">Infração Não Transmitida</param>
        /// <returns>Id da Infração Não Transmitida inserida</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("InfracaoNaoTransmitida")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirInfracaoNaoTransmitida(InfracaoNaoTransmitidaViewModel infracaoNaoTransmitida)
        {
            try
            {
                var result = await _infracaoApplicationService.InserirInfracaoNaoTransmitida(infracaoNaoTransmitida);

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
        /// Cadastro de Infração PDF
        /// </summary>
        /// <param name="infracaoPdf">Infração PDF</param>
        /// <returns>Id da Infração PDF</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("InfracaoPDF")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirInfracaoPdf(InfracaoPdfInputModel infracaoPdf)
        {
            try
            {
                var result = await _infracaoApplicationService.InserirInfracaoPdf(infracaoPdf);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastro de Infração de Pessoa
        /// </summary>
        /// <param name="infracaoPessoa">Infração</param>
        /// <returns>Id da Infração inserida</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("InfracaoPessoa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirInfracaoPessoa(InfracaoPessoaViewModel infracaoPessoa)
        {
            try
            {
                var result = await _infracaoApplicationService.InserirInfracaoPessoa(infracaoPessoa);

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
        /// Consulta Dicionário de Tipos de Infrações
        /// </summary>
        /// <returns>Lista de Dicionário de Tipos de Infrações</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Dicionario/TiposDeInfracoes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TipoInfracaoViewModel>>> ObterDicionarioDeTiposDeInfracoes()
        {
            try
            {
                var result = await _infracaoApplicationService.ObterDicionarioDeTiposDeInfracoes();

                if (result == null || !result.Any())
                    return NotFound("Dicionário de Infrações sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Equipamentos de Registro de Infrações
        /// </summary>
        /// <returns>Lista de de Equipamentos de Registro de Infrações</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Equipamentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>>> ObterEquipamentosDeRegistroDeInfracoes()
        {
            try
            {
                var result = await _infracaoApplicationService.ObterEquipamentosDeRegistroDeInfracoes();

                if (result == null || !result.Any())
                    return NotFound("Equipamentos de Registro de Infrações não encontrados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Equipamentos de Registro de Infrações por Código da Infração
        /// </summary>
        /// <param name="codigoInfracao">Código da Infração</param>
        /// <returns>Lista de de Equipamentos de Registro de Infrações</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Equipamentos/{codigoInfracao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EquipamentoDeRegistroDeInfracaoViewModel>>> ObterEquipamentosDeRegistroDeInfracoesPorInfracao(int codigoInfracao)
        {
            try
            {
                var result = await _infracaoApplicationService.ObterEquipamentosDeRegistroDeInfracoesPorInfracao(codigoInfracao);

                if (result == null || !result.Any())
                    return NotFound("Equipamentos de Registro de Infrações não encontrados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Anexos da Infração
        /// </summary>
        /// <param name="ait">AIT da Infração</param>
        /// <returns>Lista de Anexos da Infração</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("InfracaoAnexos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InfracaoAnexoViewModel>>> ObterInfracaoAnexosPorIdInfracao(string ait)
        {
            try
            {
                var result = await _infracaoApplicationService.ObterInfracaoAnexosPorIdInfracao(ait);

                if (result == null || !result.Any())
                    return NotFound("Anexos da Infração não encontrados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consultar Infrações Não Transmitidas
        /// </summary>
        /// <returns>Lista de Infrações Não Transmitidas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("InfracoesNaoTransmitidas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InfracaoNaoTransmitidaViewModel>>> ObterInfracoesNaoTransmitidas()
        {
            try
            {
                var result = await _infracaoApplicationService.ObterInfracoesNaoTransmitidas();

                if (result == null || !result.Any())
                    return NotFound("Infrações Não Transmitidas não encontradas");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consultar Infrações Não Transmitidas de Pessoas
        /// </summary>
        /// <returns>Lista de Infrações Não Transmitidas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("InfracoesNaoTransmitidasPessoas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<InfracaoNaoTransmitidaViewModel>>> ObterInfracoesNaoTransmitidasPessoas()
        {
            try
            {
                var result = await _infracaoApplicationService.ObterInfracoesNaoTransmitidasPessoas();

                if (result == null || !result.Any())
                    return NotFound("Infrações Não Transmitidas não encontradas");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion Public Methods
    }
}