using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class TcTamaParametrosViewModel
    {
        public class TcTamaParametrosResponse
        {
            public List<ParametroViewModel> Memoria { get; set; }
            public List<ParametroViewModel> Orientacao { get; set; }
            public List<ParametroViewModel> CapacidadeMotora { get; set; }
            public List<ParametroViewModel> Atitude { get; set; }
            public List<ParametroViewModel> Aparencia { get; set; }
            public List<ParametroViewModel> DescricaoCondutor { get; set; }
        }

        public class ParametroViewModel
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Descricao { get; set; }
        }
    }
}