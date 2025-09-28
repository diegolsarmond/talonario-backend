using System;
using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class TamaViewModel
    {
        public int? Id { get; set; }
        public string NumeroTAMA { get; set; }
        public string versaoApp { get; set; }
        public string TermoInput { get; set; }
        public string NumeroReciboRetencaoTalonario { get; set; }
        public string CepLocalRecolhimento { get; set; }
        public string EnderecoLocalRecolhimento { get; set; }
        public string MunicipioUfLocalRecolhimento { get; set; }
        public DateTime? DataHoraLocalRecolhimento { get; set; }
        public string LatitudeLocalRecolhimento { get; set; }
        public string LongitudeLocalRecolhimento { get; set; }
        public string TransporteLocalRecolhimento { get; set; }
        public string PlacaVeiculo { get; set; }
        public string PaisVeiculo { get; set; }
        public string MunicipioVeiculo { get; set; }
        public string UfVeiculo { get; set; }
        public string RenavamVeiculo { get; set; }
        public string MarcaModeloVeiculo { get; set; }
        public string EspecieVeiculo { get; set; }
        public string CategoriaVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string NomeCondutor { get; set; }
        public string CpfCondutor { get; set; }
        public string RgCondutor { get; set; }
        public string CnhCondutor { get; set; }
        public string TelefoneCondutor { get; set; }
        public string CepCondutor { get; set; }
        public string EnderecoCondutor { get; set; }
        public string MunicipioUfCondutor { get; set; }
        public string NomeCondutorEntregue { get; set; }
        public string CpfCondutorEntregue { get; set; }
        public string RgCondutorEntregue { get; set; }
        public string CnhCondutorEntregue { get; set; }
        public string TelefoneCondutorEntregue { get; set; }
        public string CepCondutorEntregue { get; set; }
        public string EnderecoCondutorEntregue { get; set; }
        public string MunicipioUfCondutorEntregue { get; set; }
        public string Observacoes { get; set; }
        public string MatriculaAgente { get; set; }
        public string MatriculaTestemunha1 { get; set; }
        public string MatriculaTestemunha2 { get; set; }
        public string EstadoGeralLatariaPintura { get; set; }
        public bool EquipamentosObrigatoriosAusentes { get; set; }
        public string ObjetosEncontradosVeiculo { get; set; }
        public bool VeiculoEntregueComChave { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public string UsuarioInclusao { get; set; }
        public DateTime? DataHoraAssinou { get; set; }
        public string MatriculaAssinou { get; set; }
        public DateTime? DataHoraCancelou { get; set; }
        public string MatriculaCancelou { get; set; }
        public string CodigoVerificador { get; set; }
        public string ObservacoesAdicionais { get; set; }
        public string NumeroCondutor { get; set; }
        public string BairroCondutor { get; set; }
        public string NumeroLocalInfracao { get; set; }
        public string BairroLocalInfracao { get; set; }
        public string Chassi { get; set; }
        public char? VeiculoEmplacado { get; set; }
        public IEnumerable<TermoAdocaoMedidaAdministrativa_AutosInfracaoViewModel> AutosInfracao { get; set; }
        public IEnumerable<TermoAdocaoMedidaAdministrativa_DocumentosRecolhidosViewModel> DocumentosRecolhidos { get; set; }
        public IEnumerable<TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentesViewModel> EquipamentosAusentes { get; set; }
    }

    public class TamaInputCompletoViewModel
    {
        public string Id { get; set; }
        public string IdTipoInfracao { get; set; }
        public string ArtigoCodigo { get; set; }
        public string ArtigoDesdobramento { get; set; }
        public string ArtigoCompetencia { get; set; }
        public string ArtigoNatureza { get; set; }
        public string Artigo { get; set; }
        public string ArtigoInfrator { get; set; }
        public string ArtigoDescricao { get; set; }
        public string ArtigoDescricaoCompleta { get; set; }
        public bool ArtigoApreensaoVeiculo { get; set; }
        public bool ArtigoSuspenderCNH { get; set; }
        public bool ArtigoApresentaCondutor { get; set; }
        public bool ArtigoRemoverVeiculo { get; set; }
        public bool ArtigoConfiscatePlates { get; set; }
        public bool ArtigoCargaExcessiva { get; set; }
        public bool ArtigoRequerDocumento { get; set; }
        public bool ArtigoRequerEquipamento { get; set; }
        public bool ArtigoEmVigencia { get; set; }
        public DateTime ArtigoInicioVigencia { get; set; }
        public string ArtigoFinalVigencia { get; set; }
        public int ArtigoPontos { get; set; }
        public decimal ArtigoValorMulta { get; set; }

        public string VeiculoPlaca { get; set; }
        public string VeiculoPlacaChassi { get; set; }
        public string VeiculoModelo { get; set; }
        public string VeiculoChassi { get; set; }
        public string VeiculoEspecie { get; set; }
        public string VeiculoCor { get; set; }
        public string VeiculoEmplEstado { get; set; }
        public string VeiculoEmplCidade { get; set; }
        public string VeiculoPais { get; set; }
        public string RenavamVeiculo { get; set; }

        public int LocalId { get; set; }
        public string LocalCEP { get; set; }
        public string CodigoMunicipio { get; set; }
        public string LocalRua { get; set; }
        public string LocalBairro { get; set; }
        public string LocalNumero { get; set; }
        public string LocalComplemento { get; set; }
        public string LocalCidade { get; set; }
        public string LocalEstado { get; set; }

        public DateTime AbordadoEm { get; set; }
        public string AbordadoPor { get; set; }
        public int AbordadoPorId { get; set; }
        public string AbordadoPeloOrgao { get; set; }
        public string AbordadoPeloIdOrgao { get; set; }
        public string AbordadoPeloIdOrgaoCompetencia { get; set; }

        public bool EquipamentoLavrarTC { get; set; }
        public string EquipamentoNome { get; set; }
        public string EquipamentoModelo { get; set; }
        public string EquipamentoMarca { get; set; }
        public string EquipamentoNumero { get; set; }
        public string EquipamentoTestNumero { get; set; }
        public string EquipamentoUnidadeMedida { get; set; }
        public string EquipamentoLimit { get; set; }
        public string EquipamentoDetectado { get; set; }
        public string EquipamentoConsiderado { get; set; }

        public string ProprietarioCNHCategoria { get; set; }
        public DateTime? ProprietarioCNHValidade { get; set; }
        public string ProprietarioCNHEstado { get; set; }
        public int ProprietarioDocumentoTipo { get; set; }
        public string ProprietarioDocumentoNome { get; set; }
        public string ProprietarioNDocumento { get; set; }
        public DateTime? ProprietarioDataNascimento { get; set; }
        public string ProprietarioNome { get; set; }
        public string ProprietarioNomePai { get; set; }
        public string ProprietarioNomeMae { get; set; }
        public string ProprietarioCNHNumero { get; set; }
        public string ProprietarioSexo { get; set; }
        public bool ProprietarioFromApi { get; set; }
        public bool ProprietarioVinculado { get; set; }

        public bool CondutorFoiAbordado { get; set; }
        public bool CondutorEProprietario { get; set; }
        public bool CondutorFoiIdentificado { get; set; }
        public bool CondutorPossuiCNH { get; set; }
        public string CondutorNome { get; set; }
        public string ConductorDataNascimento { get; set; }
        public string ConductorSexo { get; set; }
        public string ConductorNomeMae { get; set; }
        public string ConductorNomePai { get; set; }
        public string ConductorCategoria { get; set; }
        public string ConductorDataValidade { get; set; }
        public int CondutorDocumentoTipo { get; set; }
        public string CondutorDocumentoTipoNome { get; set; }
        public string CondutorDocumentoNumero { get; set; }
        public string CnhNumero { get; set; }
        public string CondutorPais { get; set; }
        public string CnhEstado { get; set; }
        public bool CnhNacional { get; set; }

        public DateTime DataAplicacao { get; set; }
        public string Observacoes { get; set; }
        public string Assinatura { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public string NomeAssinante { get; set; }
        public string IdAssinante { get; set; }
        public string CpfAssinante { get; set; }
        public string HistoricoModificacao { get; set; }
        public string condutorCPF { get; set; }
        public string AplicadoPor { get; set; }
        public int AplicadoPorId { get; set; }
        public string AplicadoPorCPF { get; set; }
        public string OrgaoAplicador { get; set; }
        public string AplicadoPorMatricula { get; set; }
        public string IdOrgaoAplicador { get; set; }
        public string IdOrgaoAplicadorCompetencia { get; set; }
        public string matriculaAgenteAssinante { get; set; }
        public string TesteReprovado { get; set; }
        public DateTime DataEHora { get; set; }
        public string Anexos { get; set; }
        public string ProcessamentoFalha { get; set; }
        public string VersaoApp { get; set; }
        public object TermoConstatacao { get; set; }

        public ReciboRetencaoViewModel ReciboRetencao { get; set; }
    }

    public class ReciboRetencaoViewModel
    {
        public string AIT { get; set; }
        public List<OpcaoViewModel> Opcoes { get; set; }
        public bool AusenciaEquipamentoObrigatorio { get; set; }
        public bool VeiculoRemovido { get; set; }
        public bool RemovidoComChave { get; set; }
        public bool ObjetosEncontrados { get; set; }
        public string DescricaoObjetosEncontrados { get; set; }
        public string Observations { get; set; }
        public string CondutorCPF { get; set; }
        public string CondutorNome { get; set; }
        public string CondutorCNH { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public string AssinaturaAgente { get; set; }
        public string NomeAgenteAssinante { get; set; }
        public string MatriculaAgenteAssinante { get; set; }
        public string CpfAgenteAssinante { get; set; }
        public string AssinaturaCondutor { get; set; }
    }

    public class OpcaoViewModel
    {
        public int Id { get; set; }
        public string IdRR { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public object Valor { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_AutosInfracaoViewModel
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_DocumentosRecolhidosViewModel
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
    }

    public class TermoAdocaoMedidaAdministrativa_EquipamentosObrigatoriosAusentesViewModel
    {
        public int Id { get; set; }
        public int IdTama { get; set; }
        public int IdEquipamentoObrigatorio { get; set; }
        public string EquipamentoObrigatorio { get; set; }
    }

    public class TermoAdocaoValoresViewModel
    {
        public List<ItemDropdownViewModel> EstadoGeralLatariaPintura { get; set; }
        public List<ItemDropdownViewModel> TransporteLocalRecolhimento { get; set; }
        public List<ItemDropdownViewModel> DocumentosPossiveis { get; set; }
        public List<ItemDropdownViewModel> DocumentosRecolhidos { get; set; }
    }

    public class DocumentoRecolhidoDropdownViewModel
    {
        public string Value { get; set; }
        public string Id { get; set; }
        public string Tipo { get; set; }
    }

    public class DocumentoViewModel
    {
        public int IdDocumentoRecolhido { get; set; }
        public string Titulo { get; set; }
    }

    public class ItemDropdownViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }

    public class ItensAgrupadosViewModel
    {
        public IEnumerable<ItemDropdownViewModel> EstadoGeralLatariaPintura { get; set; }
        public IEnumerable<ItemDropdownViewModel> TransporteLocalRecolhimento { get; set; }
        public IEnumerable<ItemDropdownViewModel> DocumentosPossiveis { get; set; }
        public IEnumerable<ItemDropdownViewModel> DocumentosRecolhidos { get; set; } = new List<ItemDropdownViewModel>();
    }

    public class TamaCompletoInput
    {
        public TamaViewModel Tama { get; set; }
        public TamaInputCompletoViewModel Complemento { get; set; }
    }
}

// /// <summary>
// /// DTO formatado pro frontend Utilizar conforme a estrutura do mesmo.
// /// </summary>
//public class TamaInputDto
// {
//     public int id { get; set; }
//     public string versaoApp { get; set; }
//     public string NumeroTAMA { get; set; }
//     public string CepLocalRecolhimento { get; set; }
//     public string EnderecoLocalRecolhimento { get; set; }
//     public string MunicipioUfLocalRecolhimento { get; set; }
//     public DateTime? DataHoraLocalRecolhimento { get; set; }
//     public string LatitudeLocalRecolhimento { get; set; }
//     public string LongitudeLocalRecolhimento { get; set; }
//     public string TransporteLocalRecolhimento { get; set; }
//     public string PlacaVeiculo { get; set; }
//     public string PaisVeiculo { get; set; }
//     public string MunicipioVeiculo { get; set; }
//     public string UfVeiculo { get; set; }
//     public string RenavamVeiculo { get; set; }
//     public string MarcaModeloVeiculo { get; set; }
//     public string EspecieVeiculo { get; set; }
//     public string CategoriaVeiculo { get; set; }
//     public string CorVeiculo { get; set; }
//     public string NomeCondutor { get; set; }
//     public string CpfCondutor { get; set; }
//     public string RgCondutor { get; set; }
//     public string CnhCondutor { get; set; }
//     public string TelefoneCondutor { get; set; }
//     public string CepCondutor { get; set; }
//     public string EnderecoCondutor { get; set; }
//     public string MunicipioUfCondutor { get; set; }
//     public string NomeCondutorEntregue { get; set; }
//     public string CpfCondutorEntregue { get; set; }
//     public string RgCondutorEntregue { get; set; }
//     public string CnhCondutorEntregue { get; set; }
//     public string TelefoneCondutorEntregue { get; set; }
//     public string CepCondutorEntregue { get; set; }
//     public string EnderecoCondutorEntregue { get; set; }
//     public string MunicipioUfCondutorEntregue { get; set; }
//     public string Observacoes { get; set; }
//     public string MatriculaAgente { get; set; }
//     public string MatriculaTestemunha1 { get; set; }
//     public string MatriculaTestemunha2 { get; set; }
//     public string EstadoGeralLatariaPintura { get; set; }
//     public bool EquipamentosObrigatoriosAusentes { get; set; }
//     public string ObjetosEncontradosVeiculo { get; set; }
//     public bool VeiculoEntregueComChave { get; set; }
//     public DateTime DataHoraInclusao { get; set; }
//     public string UsuarioInclusao { get; set; }
//     public DateTime? DataHoraAssinou { get; set; }
//     public string MatriculaAssinou { get; set; }
//     public DateTime? DataHoraCancelou { get; set; }
//     public string MatriculaCancelou { get; set; }
//     public string CodigoVerificador { get; set; }
//     public string ObservacoesAdicionais { get; set; }
//     public string NumeroCondutor { get; set; }
//     public string BairroCondutor { get; set; }
//     public string NumeroLocalInfracao { get; set; }
//     public string BairroLocalInfracao { get; set; }
//     public string Chassi { get; set; }
//     public char? VeiculoEmplacado { get; set; }
//     public DateTime? DataLocalRecolhimentoDate { get; set; }
//     public DateTime? DataCancelamentoDate { get; set; }
//     public List<AutoInfracaoDto> AutosInfracao { get; set; }
//     public List<DocumentoRecolhidoDto> DocumentosRecolhidos { get; set; }
//     public List<EquipamentoAusenteDto> EquipamentosAusentes { get; set; }
//     public string assinaturaAgente { get; set; }
//     public string assinaturaCondutor { get; set; }
//     public string assinaturaTestemunha1 { get; set; }
//     public string assinaturaTestemunha2 { get; set; }
//     public DateTime? dataAssinatura { get; set; }
// }

// public class AutoInfracaoDto { public string Numero { get; set; }

// [StringLength(5, ErrorMessage = "O tipo não pode ter mais de 5 caracteres.")] public string Tipo
// { get; set; } }

// public class DocumentoRecolhidoDto { public string documento { get; set; } public string Numero {
// get; set; } }

// public class EquipamentoAusenteDto { public int idEquipamento { get; set; } public string nome {
// get; set; } public string EquipamentoObrigatorio { get; set; }

// }