namespace Talonario.Api.Server.Application.Entities
{
    public class VeiculoAbordadoEntity
    {
        #region Public Constructors

        public VeiculoAbordadoEntity()
        {
        }

        public VeiculoAbordadoEntity(
            int id,
            string placa,
            string json
        )
        {
            Id = id;
            Placa = placa;
            JSON = json;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Id { get; set; }

        public string JSON { get; set; }

        public string Placa { get; set; }

        #endregion Public Properties
    }
}