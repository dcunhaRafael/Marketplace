using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarLimitesCreditoItem {
        public string id_pessoa { get; set; }
        public string nm_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string vl_lim_credito { get; set; }
        public string vl_disponivel_credito_ress { get; set; }
        public string dt_validade_tomador { get; set; }
        public string dt_validade_compliance { get; set; }
        public List<TomadorBuscarLimitesCreditoSubLimiteItem> Sublimites { get; set; }
        public List<TomadorBuscarLimitesCreditoGrupoPaisItem> TomadoresGrupoPais { get; set; }
    }
}