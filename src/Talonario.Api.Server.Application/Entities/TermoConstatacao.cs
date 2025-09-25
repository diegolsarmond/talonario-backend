using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    public class TermoConstatacao
    {
        public int? Id { get; set; }
        public string NumeroTermoConstatacao { get; set; }
        public string NumeroTermoConstatacaoTalonario { get; set; }
        public string Situacao { get; set; } = "ASSINADO";
        public string NomeCondutor { get; set; }
        public string CpfCondutor { get; set; }
        public string RgCondutor { get; set; }
        public string CnhCondutor { get; set; }
        public string TelefoneCondutor { get; set; }
        public string CepCondutor { get; set; }
        public string EnderecoCondutor { get; set; }
        public string MunicipioUfCondutor { get; set; }
        public string PlacaVeiculo { get; set; }
        public string PaisVeiculo { get; set; }
        public string MunicipioVeiculo { get; set; }
        public string UfVeiculo { get; set; }
        public string RenavamVeiculo { get; set; }
        public string MarcaModeloVeiculo { get; set; }
        public string EspecieVeiculo { get; set; }
        public string CategoriaVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string CepLocalInfracao { get; set; }
        public string EnderecoLocalInfracao { get; set; }
        public string MunicipioUfLocalInfracao { get; set; }
        public DateTime? DataHoraLocalInfracao { get; set; }
        public string LatitudeLocalInfracao { get; set; }
        public string LongitudeLocalInfracao { get; set; }
        public string Observacoes { get; set; }
        public string MatriculaAgente { get; set; }
        public string NomeAgente { get; set; }
        public string MatriculaTestemunha1 { get; set; }
        public string NomeTestemunha1 { get; set; }
        public string MatriculaTestemunha2 { get; set; }
        public string NomeTestemunha2 { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string UsuarioInclusao { get; set; }
        public DateTime? DataHoraAssinou { get; set; }
        public string MatriculaAssinou { get; set; }
        public DateTime? DataHoraCancelou { get; set; }
        public string MatriculaCancelou { get; set; }
        public int? Comprovante { get; set; }
        public string CodigoVerificador { get; set; }
        public string ObservacoesAdicionais { get; set; }
        public string NumeroCondutor { get; set; }
        public string BairroCondutor { get; set; }
        public string NumeroLocalInfracao { get; set; }
        public string BairroLocalInfracao { get; set; }
        public int CondicaoCondutor { get; set; }
        public int SubstanciaIdentificada { get; set; }
        public int TestesOferecidos { get; set; }
        public string Chassi { get; set; }

        [Column(TypeName = "char(1)")]
        public string VeiculoEmplacado { get; set; }

        public string Observacao { get; set; }

        public List<RelatoCondutor> RelatosCondutor { get; set; }
        public List<AvaliacaoCondutor> AvaliacaoCondutor { get; set; }
        public List<AutoInfracao> AutosInfracao { get; set; }
    }

    public class RelatoCondutor
    {
        public int Id { get; set; }
        public int IdTermoConstatacao { get; set; }
        public int IdDescricaoCondutor { get; set; }
        public string DescricaoCondutor { get; set; }
        public DateTime DataHora { get; set; }
    }

    public class AvaliacaoCondutor
    {
        public int Id { get; set; }
        public int IdTermoConstatacao { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
    }

    public class AutoInfracao
    {
        public int Id { get; set; }
        public int IdTermoConstatacao { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
    }
}