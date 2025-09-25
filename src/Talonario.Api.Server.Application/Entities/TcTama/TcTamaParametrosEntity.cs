using System;

namespace Talonario.Api.Server.Application.Entities.TcTama
{
    public class TcTamaParametrosEntity
    {
        public class BaseAvaliacaoCondutor
        {
            public string Titulo { get; set; }
            public string Descricao { get; set; }
            public DateTime DataInclusao { get; set; }
            public string UsuarioInclusao { get; set; }
            public DateTime? DataAtualizacao { get; set; }
            public string UsuarioAtualizacao { get; set; }
            public bool Ativo { get; set; }
        }

        public class AvaliacaoCondutorMemoria : BaseAvaliacaoCondutor
        {
            public int IdAvaliacaoCondutorMemoria { get; set; }
        }

        public class AvaliacaoCondutorOrientacao : BaseAvaliacaoCondutor
        {
            public int IdAvaliacaoCondutorOrientacao { get; set; }
        }

        public class AvaliacaoCondutorCapacidadeMotora : BaseAvaliacaoCondutor
        {
            public int IdAvaliacaoCondutorCapacidadeMotora { get; set; }
        }

        public class AvaliacaoCondutorAtitude : BaseAvaliacaoCondutor
        {
            public int IdAvaliacaoCondutorAtitude { get; set; }
        }

        public class AvaliacaoCondutorAparencia : BaseAvaliacaoCondutor
        {
            public int IdAvaliacaoCondutorAparencia { get; set; }
        }

        public class DescricaoCondutor : BaseAvaliacaoCondutor
        {
            public int IdDescricaoCondutor { get; set; }
        }
    }
}