using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.Mappers;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class GoogleMapsApplicationService : IGoogleMapsApplicationService
    {
        #region Public Constructors

        public GoogleMapsApplicationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion Public Constructors

        #region Public Properties

        public IConfiguration _configuration { get; }

        #endregion Public Properties

        #region Public Methods

        public async Task<EnderecoViewModel> ObterEnderecoPorCep(string cep)
        {
            string googleMapsKey = _configuration["GoogleMaps:Key"];

            cep = cep?.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
            if (String.IsNullOrEmpty(cep) || cep.Length < 8)
                throw new ArgumentException(paramName: nameof(cep), message: "CEP inválido.");

            var endereco = new EnderecoViewModel();

            using (var httpClient = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={cep}&key={googleMapsKey}";
                var gmapsResponse = await httpClient.GetAsync(url);
                string gmapsResult = gmapsResponse.Content.ReadAsStringAsync().Result;

                if (gmapsResponse.StatusCode == HttpStatusCode.OK)
                {
                    GoogleMapsGeoCodeViewModel jsonResult = JsonConvert.DeserializeObject<GoogleMapsGeoCodeViewModel>(gmapsResult);

                    if (jsonResult != null && jsonResult.results?.Any() == true)
                    {
                        Result resultItem = jsonResult.results.First();
                        endereco = GoogleMapsViewModelMapper.GoogleMapsEnderecoMapper(resultItem);
                    }
                }
            }

            return endereco;
        }

        public async Task<EnderecoViewModel> ObterEnderecoPorCoordenadas(string latitude, string longitude)
        {
            string googleMapsKey = _configuration["GoogleMaps:Key"];

            if (String.IsNullOrEmpty(latitude) || String.IsNullOrEmpty(longitude))
                throw new ArgumentException(message: "Coordenadas inválidas.");

            var endereco = new EnderecoViewModel();

            using (var httpClient = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={googleMapsKey}";
                var gmapsResponse = await httpClient.GetAsync(url);
                string gmapsResult = gmapsResponse.Content.ReadAsStringAsync().Result;

                if (gmapsResponse.StatusCode == HttpStatusCode.OK)
                {
                    GoogleMapsGeoCodeViewModel jsonResult = JsonConvert.DeserializeObject<GoogleMapsGeoCodeViewModel>(gmapsResult);

                    if (jsonResult != null && jsonResult.results?.Any() == true)
                    {
                        Result resultItem = jsonResult.results.First();
                        endereco = GoogleMapsViewModelMapper.GoogleMapsEnderecoMapper(resultItem);
                    }
                }
            }

            return endereco;
        }

        #endregion Public Methods
    }
}