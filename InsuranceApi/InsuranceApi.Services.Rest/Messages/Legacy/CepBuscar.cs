using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class CepBuscarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<CepBuscarItem> Consulta_CEP { get; set; }
    }

    public class CepBuscarItem {
        public string nm_endereco { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string cd_uf { get; set; }
        public string nm_uf { get; set; }
        public string nm_cep { get; set; }

        public string id_local { get; set; }
    }
}
