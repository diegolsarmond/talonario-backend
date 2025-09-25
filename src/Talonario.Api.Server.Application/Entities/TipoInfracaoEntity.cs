using System;

namespace Talonario.Api.Server.Application.Entities
{
    /// <summary>
    /// [Table("Inf_TipoInfracao")]
    /// [Table("Competencia")]
    /// [Table("Inf_NaturezaInfracao")]
    /// [Table("Responsabilidade")]
    /// [Table("inf_TipoInstrumentoMarcaModelo")]
    /// </summary>
    public class TipoInfracaoEntity
    {
        #region Public Constructors

        public TipoInfracaoEntity()
        {
        }

        public TipoInfracaoEntity(
            int idTipoInfracao,
            int codigoInfracao,
            int desdobramento,
            string competencia,
            string artigo,
            string infrator,
            int pontos,
            decimal valor,
            string natureza,
            string descricaoResumida,
            string descricaoCompleta,
            string equipamento,
            int? codigoEquipamento,
            bool? requerAnexo,
            bool? requerEquipamento,
            string retemVeiculo,
            string apresentaCondutor,
            string apreensaoPlaca,
            string transbordoCarga,
            string apreensaoVeiculo,
            string suspensaoCarteira,
            DateTime dataIniVigencia,
            DateTime? dataFimVigencia,
            DateTime dataInclusao,
            bool ativo
        )
        {
            IdTipoInfracao = idTipoInfracao;
            CodigoInfracao = codigoInfracao;
            Desdobramento = desdobramento;
            Competencia = competencia;
            Artigo = artigo;
            Infrator = infrator;
            Pontos = pontos;
            Valor = valor;
            Natureza = natureza;
            DescricaoResumida = descricaoResumida;
            DescricaoCompleta = descricaoCompleta;
            Equipamento = equipamento;
            CodigoEquipamento = codigoEquipamento;
            RequerAnexo = requerAnexo ?? false;
            RequerEquipamento = requerEquipamento ?? false;
            RetemVeiculo = retemVeiculo;
            ApresentaCondutor = apresentaCondutor;
            ApreensaoPlaca = apreensaoPlaca;
            TransbordoCarga = transbordoCarga;
            ApreensaoVeiculo = apreensaoVeiculo;
            SuspensaoCarteira = suspensaoCarteira;
            DataIniVigencia = dataIniVigencia;
            DataFimVigencia = dataFimVigencia;
            DataInclusao = dataInclusao;
            Ativo = ativo;
        }

        #endregion Public Constructors

        #region Public Properties

        public string ApreensaoPlaca { get; set; }

        public string ApreensaoVeiculo { get; set; }

        public string ApresentaCondutor { get; set; }

        public string Artigo { get; set; }

        public bool Ativo { get; set; }

        public int? CodigoEquipamento { get; set; }

        public int CodigoInfracao { get; set; }

        public string Competencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataIniVigencia { get; set; }

        public string DescricaoCompleta { get; set; }

        public string DescricaoResumida { get; set; }

        public int Desdobramento { get; set; }

        public string Equipamento { get; set; }

        public int IdTipoInfracao { get; set; }

        public string Infrator { get; set; }

        public string Natureza { get; set; }

        public int Pontos { get; set; }

        public bool? RequerAnexo { get; set; }

        public bool? RequerEquipamento { get; set; }

        public string RetemVeiculo { get; set; }

        public string SuspensaoCarteira { get; set; }

        public string TransbordoCarga { get; set; }

        public decimal Valor { get; set; }

        #endregion Public Properties
    }
}