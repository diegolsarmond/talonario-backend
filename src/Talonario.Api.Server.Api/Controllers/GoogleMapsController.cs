using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Controllers
{
    /// <summary>
    /// GoogleMapsController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api_v4/[controller]")]
    public class GoogleMapsController : ControllerBase
    {
        #region Private Fields

        private readonly IGoogleMapsApplicationService _googleMapsApplicationService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="googleMapsApplicationService"></param>
        public GoogleMapsController(IGoogleMapsApplicationService googleMapsApplicationService)
        {
            this._googleMapsApplicationService = googleMapsApplicationService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Consulta Endereço por CEP
        /// </summary>
        /// <param name="cep">CEP</param>
        /// <returns>Lista de Endereço ViewModel</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Endereco/cep/{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnderecoViewModel>> ObterEnderecoPorCep(string cep)
        {
            try
            {
                var result = await _googleMapsApplicationService.ObterEnderecoPorCep(cep);

                if (result == null || result?.Cep == null)
                    return NotFound("Endereço não encontrado");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Consulta Endereço por Coordenadas de Geolocalização (Latitude e Longitude)
        /// </summary>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <returns>Lista de Endereço ViewModel</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("Endereco/geo/lat/{latitude}/long/{longitude}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnderecoViewModel>> ObterEnderecoPorCoordenadas(string latitude, string longitude)
        {
            try
            {
                var result = await _googleMapsApplicationService.ObterEnderecoPorCoordenadas(latitude, longitude);

                if (result == null || result?.Cep == null)
                    return NotFound("Endereço não encontrado");

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