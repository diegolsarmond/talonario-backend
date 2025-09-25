using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Talonario.Api.Server.Application.Entities
{
    public class TamaEntity
    {
        public int? Id { get; set; }

        [StringLength(20)]
        public string NumeroTAMA { get; set; }

        public string NumeroReciboRetencaoTalonario { get; set; }

        [StringLength(10)]
        public string CepLocalRecolhimento { get; set; }

        [StringLength(200)]
        public string EnderecoLocalRecolhimento { get; set; }

        [StringLength(200)]
        public string MunicipioUfLocalRecolhimento { get; set; }

        public DateTime? DataHoraLocalRecolhimento { get; set; }

        [StringLength(20)]
        public string LatitudeLocalRecolhimento { get; set; }

        [StringLength(20)]
        public string LongitudeLocalRecolhimento { get; set; }

        [StringLength(50)]
        public string TransporteLocalRecolhimento { get; set; }

        [StringLength(7)]
        public string PlacaVeiculo { get; set; }

        [StringLength(50)]
        public string PaisVeiculo { get; set; }

        [StringLength(200)]
        public string MunicipioVeiculo { get; set; }

        [StringLength(2)]
        public string UfVeiculo { get; set; }

        [StringLength(11)]
        public string RenavamVeiculo { get; set; }

        [StringLength(100)]
        public string MarcaModeloVeiculo { get; set; }

        [StringLength(50)]
        public string EspecieVeiculo { get; set; }

        [StringLength(50)]
        public string CategoriaVeiculo { get; set; }

        [StringLength(50)]
        public string CorVeiculo { get; set; }

        [StringLength(200)]
        public string NomeCondutor { get; set; }

        [StringLength(11)]
        public string CpfCondutor { get; set; }

        [StringLength(20)]
        public string RgCondutor { get; set; }

        [StringLength(12)]
        public string CnhCondutor { get; set; }

        [StringLength(15)]
        public string TelefoneCondutor { get; set; }

        [StringLength(10)]
        public string CepCondutor { get; set; }

        [StringLength(200)]
        public string EnderecoCondutor { get; set; }

        [StringLength(200)]
        public string MunicipioUfCondutor { get; set; }

        [StringLength(200)]
        public string NomeCondutorEntregue { get; set; }

        [StringLength(11)]
        public string CpfCondutorEntregue { get; set; }

        [StringLength(20)]
        public string RgCondutorEntregue { get; set; }

        [StringLength(12)]
        public string CnhCondutorEntregue { get; set; }

        [StringLength(15)]
        public string TelefoneCondutorEntregue { get; set; }

        [StringLength(10)]
        public string CepCondutorEntregue { get; set; }

        [StringLength(200)]
        public string EnderecoCondutorEntregue { get; set; }

        [StringLength(200)]
        public string MunicipioUfCondutorEntregue { get; set; }

        [StringLength(500)]
        public string Observacoes { get; set; }

        [StringLength(20)]
        public string MatriculaAgente { get; set; }

        [StringLength(20)]
        public string MatriculaTestemunha1 { get; set; }

        [StringLength(20)]
        public string MatriculaTestemunha2 { get; set; }

        [StringLength(100)]
        public string EstadoGeralLatariaPintura { get; set; }

        public bool EquipamentosObrigatoriosAusentes { get; set; }

        [StringLength(500)]
        public string ObjetosEncontradosVeiculo { get; set; }

        public bool VeiculoEntregueComChave { get; set; }

        public DateTime DataHoraInclusao { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioInclusao { get; set; }

        public DateTime? DataHoraAssinou { get; set; }

        [StringLength(20)]
        public string MatriculaAssinou { get; set; }

        public DateTime? DataHoraCancelou { get; set; }

        [StringLength(20)]
        public string MatriculaCancelou { get; set; }

        [StringLength(6)]
        public string CodigoVerificador { get; set; }

        [StringLength(500)]
        public string ObservacoesAdicionais { get; set; }

        [StringLength(255)]
        public string NumeroCondutor { get; set; }

        [StringLength(255)]
        public string BairroCondutor { get; set; }

        [StringLength(255)]
        public string NumeroLocalInfracao { get; set; }

        [StringLength(255)]
        public string BairroLocalInfracao { get; set; }

        [StringLength(300)]
        public string Chassi { get; set; }

        public char? VeiculoEmplacado { get; set; }

        public DateTime? DataLocalRecolhimentoDate { get; set; }

        public DateTime? DataCancelamentoDate { get; set; }

        public IEnumerable<TermoAdocaoMedidaAdministrativa_AutosInfracao> AutosInfracao { get; set; }
        public IEnumerable<TermoAdocaoMedidaAdministrativa_DocumentosRecolhidos> DocumentosRecolhidos { get; set; }
        public List<TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentes> EquipamentosObrigatoriosAusentesLista { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_AutosInfracao
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_DocumentosRecolhidos
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentes
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public int IdEquipamentoObrigatorio { get; set; }
        public string EquipamentoObrigatorio { get; set; }
    }
}