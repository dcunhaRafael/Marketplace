using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class TomadorBuscarParametrosCalculoResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<TomadorBuscarParametrosCalculoItem> Tomador_BuscarParametrosCalculo { get; set; }
    }

    public class TomadorBuscarParametrosCalculoItem {
        public string vl_premio_minimo { get; set; }
        public string vl_taxa_risco { get; set; }
        public string pe_comissao { get; set; }
    }
}
