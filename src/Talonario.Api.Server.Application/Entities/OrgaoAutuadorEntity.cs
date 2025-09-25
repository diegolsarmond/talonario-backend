using System;

namespace Talonario.Api.Server.Application.Entities
{
    public class OrgaoAutuadorEntity
    {
        #region Public Constructors

        public OrgaoAutuadorEntity()
        {
        }

        public OrgaoAutuadorEntity(
            long codigoOrgaoAutuador,
            string cnpjOrgaoAutuador,
            string nomeOrgaoAutuador,
            string siglaOrgaoAutuador,
            string ufOrgaoAutuador,
            string tipoOrgaoAutuador,
            string codigoConvenio,
            DateTime dataInclusao
        )
        {
            CodigoOrgaoAutuador = codigoOrgaoAutuador;
            CNPJOrgaoAutuador = cnpjOrgaoAutuador;
            NomeOrgaoAutuador = nomeOrgaoAutuador;
            SiglaOrgaoAutuador = siglaOrgaoAutuador;
            UFOrgaoAutuador = ufOrgaoAutuador;
            TipoOrgaoAutuador = tipoOrgaoAutuador;
            CodigoConvenio = codigoConvenio;
            DataInclusao = dataInclusao;
        }

        #endregion Public Constructors

        #region Public Properties

        public string CNPJOrgaoAutuador { get; set; }

        public string CodigoConvenio { get; set; }

        public long CodigoOrgaoAutuador { get; set; }

        public DateTime DataInclusao { get; set; }

        public string NomeOrgaoAutuador { get; set; }

        public string SiglaOrgaoAutuador { get; set; }

        public string TipoOrgaoAutuador { get; set; }

        public string UFOrgaoAutuador { get; set; }

        #endregion Public Properties
    }
}