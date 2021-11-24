using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class CidadeConsultarRequest {
        public int cd_uf { get; set; }
        public string nm_cidade { get; set; }
    }

    public class CidadeConsultarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<CidadeConsultarItem> Consulta_Cidade { get; set; }
    }

    public class CidadeConsultarItem {
        public int cd_uf { get; set; }
        public string chave_local { get; set; }
        public string nm_cidade { get; set; }
    }
}
