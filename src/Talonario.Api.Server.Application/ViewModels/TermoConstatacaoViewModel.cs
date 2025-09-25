using System;
using System.Collections.Generic;
using Talonario.Api.Server.Application.Entities;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class TermoConstatacaoInputDto
    {
        public string id { get; set; }
        public string idTipoInfracao { get; set; }
        public string artigoCodigo { get; set; }
        public string artigoDesdobramento { get; set; }
        public string artigoCompetencia { get; set; }
        public string artigoNatureza { get; set; }
        public string artigo { get; set; }
        public string artigoInfrator { get; set; }
        public string artigoDescricao { get; set; }
        public string artigoDescricaoCompleta { get; set; }
        public bool VeiculoEmplacado { get; set; }
        public bool artigoApreensaoVeiculo { get; set; }
        public bool artigoSuspenderCNH { get; set; }
        public bool artigoApresentaCondutor { get; set; }
        public bool artigoRemoverVeiculo { get; set; }
        public bool artigoConfiscatePlates { get; set; }
        public bool artigoCargaExcessiva { get; set; }
        public bool artigoRequerDocumento { get; set; }
        public bool artigoRequerEquipamento { get; set; }
        public bool artigoEmVigencia { get; set; }
        public string artigoInicioVigencia { get; set; }
        public string artigoFinalVigencia { get; set; }
        public int artigoPontos { get; set; }
        public int artigoEquipamento { get; set; }
        public decimal artigoValorMulta { get; set; }
        public string veiculoPlaca { get; set; }
        public string veiculoPlacaChassi { get; set; }
        public string veiculoModelo { get; set; }
        public string veiculoChassi { get; set; }
        public string veiculoEspecie { get; set; }
        public string veiculoCor { get; set; }
        public string veiculoEmplEstado { get; set; }
        public string veiculoEmplCidade { get; set; }
        public string veiculoPais { get; set; }
        public int localId { get; set; }
        public string localCEP { get; set; }
        public string codigoMunicipio { get; set; }
        public string localRua { get; set; }
        public string localBairro { get; set; }
        public string localNumero { get; set; }
        public string localComplemento { get; set; }
        public string localCidade { get; set; }
        public string localEstado { get; set; }
        public string abordadoEm { get; set; }
        public string abordadoPor { get; set; }
        public int abordadoPorId { get; set; }
        public string abordadoPeloOrgao { get; set; }
        public string abordadoPeloIdOrgao { get; set; }
        public string abordadoPeloIdOrgaoCompetencia { get; set; }
        public bool equipamentoLavrarTC { get; set; }
        public string equipamentoNome { get; set; }
        public string equipamentoModelo { get; set; }
        public string equipamentoMarca { get; set; }
        public string equipamentoNumero { get; set; }
        public string equipamentoTestNumero { get; set; }
        public string equipamentoUnidadeMedida { get; set; }
        public string equipamentoLimit { get; set; }
        public string equipamentoDetectado { get; set; }
        public string equipamentoConsiderado { get; set; }
        public string proprietarioCNHCategoria { get; set; }
        public string proprietarioCNHValidade { get; set; }
        public string proprietarioCNHEstado { get; set; }
        public int proprietarioDocumentoTipo { get; set; }
        public string proprietarioDocumentoNome { get; set; }
        public string proprietarioNDocumento { get; set; }
        public string proprietarioDataNascimento { get; set; }
        public string proprietarioNome { get; set; }
        public string proprietarioNomePai { get; set; }
        public string proprietarioNomeMae { get; set; }
        public string proprietarioCNHNumero { get; set; }
        public string proprietarioSexo { get; set; }
        public bool proprietarioFromApi { get; set; }
        public bool proprietarioVinculado { get; set; }
        public bool condutorFoiAbordado { get; set; }
        public bool condutorEProprietario { get; set; }
        public bool condutorFoiIdentificado { get; set; }
        public bool condutorPossuiCNH { get; set; }
        public string condutorNome { get; set; }
        public string conductorDataNascimento { get; set; }
        public string conductorSexo { get; set; }
        public string conductorNomeMae { get; set; }
        public string conductorNomePai { get; set; }
        public string conductorCategoria { get; set; }
        public string conductorDataValidade { get; set; }
        public int condutorDocumentoTipo { get; set; }
        public string condutorDocumentoTipoNome { get; set; }
        public string condutorDocumentoNumero { get; set; }
        public string cnhNumero { get; set; }
        public string condutorPais { get; set; }
        public string cnhEstado { get; set; }
        public bool cnhNacional { get; set; }
        public string dataAplicacao { get; set; }
        public string aplicacoesAdmin { get; set; }
        public string observacoes { get; set; }
        public int equipamentoUsado { get; set; }
        public string dataCancelamento { get; set; }
        public string dataEnvio { get; set; }
        public string assinatura { get; set; }
        public string assinaturaCondutor { get; set; }
        public DateTime? dataAssinatura { get; set; }
        public string nomeAssinante { get; set; }
        public string idAssinante { get; set; }
        public string cpfAssinante { get; set; }
        public string historicoModificacao { get; set; }
        public string aplicadoPor { get; set; }
        public int aplicadoPorId { get; set; }
        public string aplicadoPorCPF { get; set; }
        public string OrgaoAplicador { get; set; }
        public string NumeroAutoInfracao { get; set; }
        public string aplicadoPorMatricula { get; set; }
        public string idOrgaoAplicador { get; set; }
        public string idOrgaoAplicadorCompetencia { get; set; }
        public string testeReprovado { get; set; }
        public string dataEHora { get; set; }
        public string anexos { get; set; }
        public string processamentoFalha { get; set; }
        public string versaoApp { get; set; }
        public string observacao { get; set; }
        public TermoConstatacaoDetalhesDto termoConstatacao { get; set; }
    }

    public class TermoConstatacaoDetalhesDto
    {
        public string idAIT { get; set; }
        public List<AvaliacaoDto> avaliacoes { get; set; }
        public string dataAssinatura { get; set; }
        public string assinaturaAgente { get; set; }
        public string nomeAgenteAssinante { get; set; }
        public string matriculaAgenteAssinante { get; set; }
        public string cpfAgenteAssinante { get; set; }
        public string assinaturaCondutor { get; set; }
        public string assinaturaTestemunha1 { get; set; }
        public string assinaturaTestemunha2 { get; set; }
        public string matriculaTestemunha1 { get; set; }
        public string nomeTestemunha1 { get; set; }
        public string cpfTestemunha1 { get; set; }
        public string matriculaTestemunha2 { get; set; }
        public string nomeTestemunha2 { get; set; }
        public string cpfTestemunha2 { get; set; }
    }

    public class AvaliacaoDto
    {
        public int id { get; set; }
        public string idTC { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
    }

    public class TermoConstatacaoViewModel
    {
        public int? Id { get; set; }
        public string NumeroTermoConstatacaoTalonario { get; set; }
        public string Situacao { get; set; } = "ASSINADO";
        public string NumeroTermoConstatacao { get; set; }
        public string Chassi { get; set; }
        public bool VeiculoEmplacado { get; set; }

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
        public int Observacao { get; set; }

        public List<AvaliacaoCondutor> AvaliacaoCondutor { get; set; } = new List<AvaliacaoCondutor>();
        public List<AutoInfracao> AutosInfracao { get; set; } = new List<AutoInfracao>();
        public List<RelatoCondutor> RelatosCondutor { get; set; } = new List<RelatoCondutor>();
    }
}

public class RelatoCondutorViewModel
{
    public int Id { get; set; }
    public int IdTermoConstatacao { get; set; }
    public int IdDescricaoCondutor { get; set; }
    public string DescricaoCondutor { get; set; }
    public DateTime DataHora { get; set; }
}

public class AvaliacaoCondutorViewModel
{
    public int Id { get; set; }
    public int IdTermoConstatacao { get; set; }
    public string Descricao { get; set; }
    public int Tipo { get; set; }
}

public class AutoInfracaoViewModel
{
    public int Id { get; set; }
    public int IdTermoConstatacao { get; set; }
    public string Numero { get; set; }
    public int Tipo { get; set; }
}

public class TermoConstatacaoListagemViewModel
{
    public int Id { get; set; }
    public string NumeroTermoConstatacao { get; set; }
    public string NomeCondutor { get; set; }
    public string CpfCondutor { get; set; }
    public string PlacaVeiculo { get; set; }
    public DateTime DataHoraInclusao { get; set; }
    public string Status => DataHoraCancelou.HasValue ? "Cancelado" : "Ativo";
    public DateTime? DataHoraCancelou { get; set; }
}

public class TermoConstatacaoFiltroViewModel
{
    public string NumeroTermo { get; set; }
    public string CpfCondutor { get; set; }
    public string PlacaVeiculo { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string Status { get; set; }
}

public class AssinaturaTermoRequest
{
    public string NumeroTc { get; set; }
    public string Matricula { get; set; }
}

public class ObservacaoTermoRequest
{
    public string NumeroTc { get; set; }
    public string Observacao { get; set; }
}

public class ComprovanteTermoRequest
{
    public int IdTermo { get; set; }
    public int Comprovante { get; set; }
}