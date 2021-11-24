using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarParametrosCalculoItem {
        public string vl_premio_minimo { get; set; }
        public string vl_taxa_risco { get; set; }
        public string pe_comissao { get; set; }
    }
}