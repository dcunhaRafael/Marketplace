using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {

    public class CoberturaBuscarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<CoberturaBuscarItem> Cobertura_Buscar { get; set; }
    }

    public class CoberturaBuscarItem {
        public string id_produto_cobertura { get; set; }
        public string nm_cobertura { get; set; }
    }
}

