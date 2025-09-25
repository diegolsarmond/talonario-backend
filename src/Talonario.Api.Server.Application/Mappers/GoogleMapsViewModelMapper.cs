using System.Linq;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application.Mappers
{
    public static class GoogleMapsViewModelMapper
    {
        #region Public Methods

        public static EnderecoViewModel GoogleMapsEnderecoMapper(Result resultItem)
        {
            EnderecoViewModel endereco;
            string postalCode = resultItem.address_components.FirstOrDefault(a => a.types.Contains("postal_code"))?.long_name;
            string numero = resultItem.address_components.FirstOrDefault(a => a.types.Contains("street_number"))?.long_name;
            string logradouro = resultItem.address_components.FirstOrDefault(a => a.types.Contains("route"))?.short_name;
            string bairro = resultItem.address_components.FirstOrDefault(a => a.types.Contains("sublocality_level_1"))?.short_name;
            string cidade = resultItem.address_components.FirstOrDefault(a => a.types.Contains("administrative_area_level_2"))?.short_name;
            string uf = resultItem.address_components.FirstOrDefault(a => a.types.Contains("administrative_area_level_1"))?.short_name;
            string pais = resultItem.address_components.FirstOrDefault(a => a.types.Contains("country"))?.long_name;

            endereco = new EnderecoViewModel(
                postalCode,
                logradouro,
                numero,
                bairro,
                cidade,
                uf,
                pais
            );

            return endereco;
        }

        #endregion Public Methods
    }
}