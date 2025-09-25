using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class TipoInfracaoViewModel
    {
        #region Public Constructors

        public TipoInfracaoViewModel(
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
            NomeEquipamento = equipamento;
            Equipamento = codigoEquipamento;
            RequerAnexo = requerAnexo ?? false;
            RequerEquipamento = requerEquipamento ?? false;
            RetemVeiculo = retemVeiculo?.ToUpper() == "S";
            ApresentaCondutor = apresentaCondutor?.ToUpper() == "S";
            ApreensaoPlaca = apreensaoPlaca?.ToUpper() == "S";
            TransbordoCarga = transbordoCarga?.ToUpper() == "S";
            ApreensaoVeiculo = apreensaoVeiculo?.ToUpper() == "S";
            SuspensaoCarteira = suspensaoCarteira?.ToUpper() == "S";
            DataIniVigencia = dataIniVigencia;
            DataFimVigencia = dataFimVigencia;
            DataInclusao = dataInclusao;
            EmVigencia = (dataIniVigencia <= DateTime.Now && (DataFimVigencia == null || DataFimVigencia >= DateTime.Now)) ? true : false;
            Ativo = ativo;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool ApreensaoPlaca { get; set; }

        public bool ApreensaoVeiculo { get; set; }

        public bool ApresentaCondutor { get; set; }

        public string Artigo { get; set; }

        public bool Ativo { get; set; }

        public int CodigoInfracao { get; set; }

        public string Competencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataIniVigencia { get; set; }

        public string DescricaoCompleta { get; set; }

        public string DescricaoResumida { get; set; }

        public int Desdobramento { get; set; }

        public bool EmVigencia { get; set; }

        public int? Equipamento { get; set; }

        public int IdTipoInfracao { get; set; }

        public string Infrator { get; set; }

        public string Natureza { get; set; }

        public string NomeEquipamento { get; set; }

        public int Pontos { get; set; }

        public bool RequerAnexo { get; set; }

        public bool RequerEquipamento { get; set; }

        public bool RetemVeiculo { get; set; }

        public bool SuspensaoCarteira { get; set; }

        public bool TransbordoCarga { get; set; }

        public decimal Valor { get; set; }

        #endregion Public Properties
    }
}