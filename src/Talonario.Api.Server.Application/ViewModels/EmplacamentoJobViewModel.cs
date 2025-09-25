using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talonario.Api.Server.Application.ViewModels
{
    /// <summary>
    /// InfracaoNaoTransmitidaJSON
    /// </summary>
    public class InfracaoNaoTransmitidaJSON
    {
        #region Public Fields

        public DateTime abordadoEm { get; set; }
        public string aplicadoPorMatricula { get; set; }
        public string aplicadorPor { get; set; }
        public string articleSuspendCNH { get; set; }
        public string artigoApreensaoVeiculo { get; set; }
        public string artigoCodigo { get; set; }
        public string artigoDesdobramento { get; set; }
        public string artigoEquipamento { get; set; }
        public string artigoRemoverVeiculo { get; set; }
        public string cnhEstado { get; set; }
        public string cnhNumero { get; set; }
        public string codigoMunicipio { get; set; }
        public string condutorDocumentoNumero { get; set; }
        public string condutorDocumentoTipo { get; set; }
        public string condutorFoiAbordado { get; set; }
        public string condutorNome { get; set; }
        public string cpfAssinante { get; set; }
        public DateTime dataAplicacao { get; set; }
        public string equipamentoConsiderado { get; set; }
        public string equipamentoDetectado { get; set; }
        public string equipamentoNome { get; set; }
        public string equipamentoUsado { get; set; }
        public string id { get; set; }
        public int idOrgaoAplicador { get; set; }
        public string localBairro { get; set; }
        public string localCEP { get; set; }
        public string localCidade { get; set; }
        public string localEstado { get; set; }
        public string localId { get; set; }
        public string localNumero { get; set; }
        public string localRua { get; set; }
        public string nomeAssinante { get; set; }
        public string observacoes { get; set; }
        public string veiculoChassi { get; set; }
        public string veiculoEmplEstado { get; set; }

        #endregion Public Fields
    }
}