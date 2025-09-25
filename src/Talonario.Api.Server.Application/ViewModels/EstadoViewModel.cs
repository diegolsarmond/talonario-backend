using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class EstadoViewModel
    {
        #region Public Constructors

        public EstadoViewModel()
        {
        }

        public EstadoViewModel(
            int id,
            string sigla,
            string nome
        )
        {
            Id = id;
            Sigla = sigla;
            Nome = nome;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static List<EstadoViewModel> ListaUnidadesFederativasBrasil()
        {
            return new List<EstadoViewModel>() {
                new EstadoViewModel() { Id = 1, Sigla = "AC", Nome = "Acre" },
                new EstadoViewModel() { Id = 2, Sigla = "AL", Nome = "Alagoas" },
                new EstadoViewModel() { Id = 3, Sigla = "AP", Nome = "Amapá" },
                new EstadoViewModel() { Id = 4, Sigla = "AM", Nome = "Amazonas" },
                new EstadoViewModel() { Id = 5, Sigla = "BA", Nome = "Bahia" },
                new EstadoViewModel() { Id = 6, Sigla = "CE", Nome = "Ceara" },
                new EstadoViewModel() { Id = 7, Sigla = "DF", Nome = "Distrito Federal" },
                new EstadoViewModel() { Id = 8, Sigla = "ES", Nome = "Espírito Santo" },
                new EstadoViewModel() { Id = 9, Sigla = "GO", Nome = "Goiás" },
                new EstadoViewModel() { Id = 10, Sigla = "MA", Nome = "Maranhão" },
                new EstadoViewModel() { Id = 11, Sigla = "MT", Nome = "Mato Grosso" },
                new EstadoViewModel() { Id = 12, Sigla = "MS", Nome = "Mato Grosso do Sul" },
                new EstadoViewModel() { Id = 13, Sigla = "MG", Nome = "Minas Gerais" },
                new EstadoViewModel() { Id = 14, Sigla = "PA", Nome = "Pará" },
                new EstadoViewModel() { Id = 15, Sigla = "PB", Nome = "Paraíba" },
                new EstadoViewModel() { Id = 16, Sigla = "PR", Nome = "Paraná" },
                new EstadoViewModel() { Id = 17, Sigla = "PE", Nome = "Pernambuco" },
                new EstadoViewModel() { Id = 18, Sigla = "PI", Nome = "Piauí" },
                new EstadoViewModel() { Id = 19, Sigla = "RJ", Nome = "Rio de Janeiro" },
                new EstadoViewModel() { Id = 20, Sigla = "RN", Nome = "Rio Grande do Norte" },
                new EstadoViewModel() { Id = 21, Sigla = "RS", Nome = "Rio Grande do Sul" },
                new EstadoViewModel() { Id = 22, Sigla = "RO", Nome = "Rondônia" },
                new EstadoViewModel() { Id = 23, Sigla = "RR", Nome = "Roraima" },
                new EstadoViewModel() { Id = 24, Sigla = "SC", Nome = "Santa Catarina" },
                new EstadoViewModel() { Id = 25, Sigla = "SP", Nome = "São Paulo" },
                new EstadoViewModel() { Id = 26, Sigla = "SE", Nome = "Sergipe" },
                new EstadoViewModel() { Id = 27, Sigla = "TO", Nome = "Tocantins" }
            };
        }

        #endregion Public Methods
    }
}