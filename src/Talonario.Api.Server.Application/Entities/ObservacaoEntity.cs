namespace Talonario.Api.Server.Application.Entities
{
    public class ObservacaoEntity
    {
        #region Public Constructors

        public ObservacaoEntity()
        {
        }

        public ObservacaoEntity(int id, string titulo, string descricao)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Descricao { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }

        #endregion Public Properties
    }
}