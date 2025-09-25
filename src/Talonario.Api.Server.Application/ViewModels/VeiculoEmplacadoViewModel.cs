using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class VeiculoEmplacadoViewModel
    {
        #region Public Constructors

        public VeiculoEmplacadoViewModel(
            string placa,
            string chassi,
            DateTime dataInclusao
        )
        {
            Placa = placa;
            Chassi = chassi;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Chassi { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Placa { get; set; }

        #endregion Public Properties
    }
}