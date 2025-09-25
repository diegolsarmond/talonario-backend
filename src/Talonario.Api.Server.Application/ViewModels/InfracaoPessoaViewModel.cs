using System;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class InfracaoPessoaViewModel
    {
        #region Public Properties

        public string AbordadoPeloIdOrgao { get; set; }
        public string AbordadoPeloOrgao { get; set; }
        public string AbordadoPeloOrgaoCompetencia { get; set; }
        public string Artigo { get; set; }
        public bool ArtigoApreensaoVeiculo { get; set; }
        public bool ArtigoApresentaCondutor { get; set; }
        public bool ArtigoCargaExcessiva { get; set; }
        public string ArtigoCodigo { get; set; }
        public string ArtigoCompetencia { get; set; }
        public bool ArtigoConfiscatePlates { get; set; }
        public string ArtigoDescricao { get; set; }
        public string ArtigoDescricaoCompleta { get; set; }
        public string ArtigoDesdobramento { get; set; }
        public bool ArtigoEmVigencia { get; set; }
        public int ArtigoEquipamento { get; set; }
        public DateTime ArtigoFinalVigencia { get; set; }
        public string ArtigoInfrator { get; set; }
        public DateTime ArtigoInicioVigencia { get; set; }
        public string ArtigoNatureza { get; set; }
        public int ArtigoPontos { get; set; }
        public bool ArtigoRemoverVeiculo { get; set; }
        public bool ArtigoRequerDocumento { get; set; }
        public bool ArtigoRequerEquipamento { get; set; }
        public bool ArtigoSuspenderCNH { get; set; }
        public double ArtigoValorMulta { get; set; }
        public string CPF { get; set; }
        public string CpfAgente { get; set; }
        public DateTime DataAbordagem { get; set; }
        public string DataNascimento { get; set; }
        public string Genero { get; set; }
        public string IdTipoInfracao { get; set; }
        public string LocalBairro { get; set; }
        public string LocalCep { get; set; }
        public string LocalCodigoMunicipio { get; set; }
        public string LocalComplemento { get; set; }
        public string LocalEstado { get; set; }
        public int LocalId { get; set; }
        public string LocalLogradouro { get; set; }
        public string LocalMunicipio { get; set; }
        public string LocalNumero { get; set; }
        public string Nome { get; set; }
        public string NomeAgente { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string NumeroAuto { get; set; }
        public string PessoaEndBairro { get; set; }
        public string PessoaEndCep { get; set; }
        public string PessoaEndCidade { get; set; }
        public string PessoaEndComplemento { get; set; }
        public string PessoaEndEstado { get; set; }
        public string PessoaEndNumero { get; set; }
        public string PessoaEndRua { get; set; }
        public string RG { get; set; }

        #endregion Public Properties
    }
}