using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class MarcaModeloTipoViewModel
    {
        #region Public Constructors

        public MarcaModeloTipoViewModel(
            int idMarca,
            int idEspecie,
            string marcaModelo,
            string especie,
            string tipoVeiculo,
            string restricaoFazendaria,
            string porte,
            string temPlacaDianteira,
            string placaPequena,
            string podeSerTaxi,
            string podeSerAmbulancia,
            string podeSerEscolar,
            DateTime dataInclusao
        )
        {
            IdMarca = idMarca;
            IdEspecie = idEspecie;
            MarcaModelo = marcaModelo;
            Especie = especie;
            TipoVeiculo = tipoVeiculo;
            RestricaoFazendaria = restricaoFazendaria;
            Porte = porte;
            TemPlacaDianteira = temPlacaDianteira;
            PlacaPequena = placaPequena;
            PodeSerTaxi = podeSerTaxi;
            PodeSerAmbulancia = podeSerAmbulancia;
            PodeSerEscolar = podeSerEscolar;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime DataInclusao { get; set; }

        public string Especie { get; set; }

        public int IdEspecie { get; set; }

        public int IdMarca { get; set; }

        public string MarcaModelo { get; set; }

        public string PlacaPequena { get; set; }

        public string PodeSerAmbulancia { get; set; }

        public string PodeSerEscolar { get; set; }

        public string PodeSerTaxi { get; set; }

        public string Porte { get; set; }

        public string RestricaoFazendaria { get; set; }

        public string TemPlacaDianteira { get; set; }

        public string TipoVeiculo { get; set; }

        #endregion Public Properties
    }
}