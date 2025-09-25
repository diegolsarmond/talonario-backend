using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// VeiculoController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class VeiculoController : ControllerBase
    {
        #region Private Fields

        private readonly ICondutorApplicationService _condutorApplicationService;
        private readonly IVeiculoApplicationService _veiculoApplicationService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="veiculoApplicationService"></param>
        /// <param name="condutorApplicationService"></param>
        public VeiculoController(IVeiculoApplicationService veiculoApplicationService,
                                 ICondutorApplicationService condutorApplicationService)
        {
            _veiculoApplicationService = veiculoApplicationService;
            _condutorApplicationService = condutorApplicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Consulta Proprietário por Placa
        /// </summary>
        /// <param name="placa">Placa do Veículo</param>
        /// <returns>Dados do Proprietário</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ConsultaProprietarioPorPlaca/{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProprietarioViewModel>> ConsultaProprietarioPorPlaca(string placa)
        {
            try
            {
                var proprietario = await _veiculoApplicationService.ConsultaProprietarioPorPlaca(placa);

                if (proprietario == null)
                    return NotFound("Veículo/Proprietário não encontrado(s)");

                return Ok(proprietario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículo por Chassi
        /// </summary>
        /// <param name="chassi">Chassi do Veículo</param>
        /// <returns>Dados do Veículo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ConsultaVeiculoPorChassi/{chassi}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoViewModel>> ConsultaVeiculoPorChassi(string chassi)
        {
            try
            {
                var veiculo = await _veiculoApplicationService.ConsultaVeiculoPorChassi(chassi);

                if (veiculo == null)
                    return NotFound("Veículo não encontrado");

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículo por Placa
        /// </summary>
        /// <param name="placa">Placa do Veículo</param>
        /// <returns>Dados do Veículo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ConsultaVeiculoPorPlaca/{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoViewModel>> ConsultaVeiculoPorPlaca(string placa)
        {
            try
            {
                var veiculo = await _veiculoApplicationService.ConsultaVeiculoPorPlaca(placa);

                if (veiculo == null)
                    return NotFound("Veículo não encontrado");

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículo por Placa e Renavam
        /// </summary>
        /// <param name="placa">Placa do Veículo</param>
        /// <param name="renavam">Renavam do Veículo</param>
        /// <returns>Dados do Veículo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ConsultaVeiculoPorPlacaERenavam/{placa}/{renavam}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoViewModel>> ConsultaVeiculoPorPlacaERenavam(string placa, string renavam)
        {
            try
            {
                var veiculo = await _veiculoApplicationService.ConsultaVeiculoPorPlacaERenavam(placa, renavam);

                if (veiculo == null)
                    return NotFound("Veículo não encontrado");

                return Ok(veiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículo por Renavam
        /// </summary>
        /// <param name="renavam">Renavam do Veículo</param>
        /// <returns>Dados do Veículo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("ConsultaVeiculoPorRenavam/{renavam}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoViewModel>> ConsultaVeiculoPorRenavam(string renavam)
        {
            try
            {
                bool renavamInvalido = !long.TryParse(renavam, out _);

                if (renavamInvalido)
                    return BadRequest("Renavam deve ter apenas números");

                var veiculo = await _veiculoApplicationService.ConsultaVeiculoPorRenavam(renavam);

                if (veiculo is not null)
                    return Ok(veiculo);

                return NotFound("Veículo não encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Dados do condutor do veículo por cpf
        /// </summary>
        /// <param name="cpf">Cpf do condutor do veículo</param>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Dados não encontrados</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("Condutor/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DadosCondutorPorCpf(string cpf)
        {
            try
            {
                var result = await _condutorApplicationService.PesquisarPorCpf(cpf);

                if (result is not null)
                {
                    return Ok(result);
                }

                result = await _condutorApplicationService.PesquisarExternaPorCpf(cpf);

                if (result is null)
                    return NotFound("Nenhum registro encontrado");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastro de Veículo Abordado
        /// </summary>
        /// <param name="veiculoAbordado">Veículo Abordado</param>
        /// <returns>Id do Veículo Abordado inserido</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("VeiculoAbordado")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> InserirVeiculoAbordado(VeiculoAbordadoViewModel veiculoAbordado)
        {
            try
            {
                var result = await _veiculoApplicationService.InserirVeiculoAbordado(veiculoAbordado);

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
        /// Consulta de Cidades, Estados e Países
        /// </summary>
        /// <returns>Lista de Cidades, Estados e Países</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Localidade/CidadeEstadoPais")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LocalidadeViewModel>>> ObterCidadeEstadoPais()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterCidadeEstadoPais();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Cidades, Estados e Países sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Cidades
        /// </summary>
        /// <returns>Lista de Cidades</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Localidade/Cidade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CidadeViewModel>>> ObterCidades()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterCidades();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Cidades sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Cores
        /// </summary>
        /// <returns>Lista de Cores</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Cores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CorViewModel>>> ObterCores()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterCores();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Cores sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Cores, Modelos e Espécies
        /// </summary>
        /// <returns>Lista de Cores, Modelos e Espécies</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("CorModeloEspecie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CorModeloEspecieViewModel>> ObterCoresModelosEspecies()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterCoresModelosEspecies();

                if (result == null)
                    return NotFound("Consulta Cores, Modelos e Espécies sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Espécies
        /// </summary>
        /// <returns>Lista de Espécies</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Especies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EspecieViewModel>>> ObterEspecies()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterEspecies();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Espécies sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Estados
        /// </summary>
        /// <returns>Lista de Estados</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Localidade/Estado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EstadoViewModel>> ObterEstados()
        {
            try
            {
                var result = _veiculoApplicationService.ObterEstados();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Estados sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Marcas e Modelos
        /// </summary>
        /// <param name="page">Página corrente para Paginação</param>
        /// <param name="limit">Limite de registros para Paginação</param>
        /// <param name="retPaginationProperties">
        /// Define o retorno do objeto com ou sem propriedades de paginação
        /// </param>
        /// <returns>Lista de Marca e Modelo</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("MarcasModelos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MarcaModeloViewModel>>> ObterMarcasModelos(int page, int limit, bool? retPaginationProperties = false)
        {
            try
            {
                var result = await _veiculoApplicationService.ObterMarcasModelos(page, limit);

                if (result == null || !result.Items.Any())
                    return NotFound("Consulta de Marca e Modelo sem resultados");

                return (retPaginationProperties == false) ? Ok(result.Items) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Marcas, Modelos, Tipos e Espécies
        /// </summary>
        /// <param name="page">Página corrente para Paginação</param>
        /// <param name="limit">Limite de registros para Paginação</param>
        /// <param name="retPaginationProperties">
        /// Define o retorno do objeto com ou sem propriedades de paginação
        /// </param>
        /// <returns>Lista de Marca, Modelos, Tipos e Espécies</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("MarcasModelosTiposEspecies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MarcaModeloTipoViewModel>>> ObterMarcasModelosTiposEspecies(int page, int limit, bool? retPaginationProperties = false)
        {
            try
            {
                var result = await _veiculoApplicationService.ObterMarcasModelosTiposEspecies(page, limit);

                if (result == null || !result.Items.Any())
                    return NotFound("Consulta completa de Marca, Modelos, Tipos e Espécies sem resultados");

                return (retPaginationProperties == false) ? Ok(result.Items) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta de Países
        /// </summary>
        /// <returns>Lista de Países</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Localidade/Pais")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PaisViewModel>>> ObterPaises()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterPaises();

                if (result == null || !result.Any())
                    return NotFound("Consulta de Países sem resultados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículo Abordado por Placa
        /// </summary>
        /// <param name="placa">Placa do Veículo Abordado</param>
        /// <returns>Veículo Abordado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("VeiculoAbordado/{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeiculoAbordadoViewModel>> ObterVeiculoAbordadoPorPlaca(string placa)
        {
            try
            {
                var result = await _veiculoApplicationService.ObterVeiculoAbordadoPorPlaca(placa);

                if (result == null)
                    return NotFound("Veículo Abordado não encontrado");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Veículos Abordados
        /// </summary>
        /// <returns>Lista de Veículos Abordados</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("VeiculosAbordados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VeiculoAbordadoViewModel>>> ObterVeiculosAbordados()
        {
            try
            {
                var result = await _veiculoApplicationService.ObterVeiculosAbordados();

                //POR SOLICITAÇÃO DO FRONT O RETORNO DESTE ENPOINT
                //EM CASO DE NÃO OBTER RESULTADOS SERÁ LISTA VAZIA, E NÃO 404.
                //if (result == null || !result.Any())
                //    return NotFound("Veículos Abordados não encontrados");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Remover Veículo Abordado por AIT
        /// </summary>
        /// <param name="placa">Placa do Veículo Abordado</param>
        /// <returns></returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("VeiculoAbordado/{placa}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> RemoverVeiculoAbordadoPorPlaca(string placa)
        {
            try
            {
                var result = await _veiculoApplicationService.RemoverVeiculoAbordadoPorPlaca(placa);

                if (!result)
                    return BadRequest("Nenhum registro foi removido");

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