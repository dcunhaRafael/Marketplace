using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarLimitesCreditoSubLimiteItem {
        public string nm_grupo_coberturas { get; set; }
        public int id_grp_sub_limite { get; set; }
        public int id_grp_sub_limite_tomador { get; set; }
        public decimal vl_sublimite_cedito { get; set; }
        public decimal vl_sublimite_credito_disponivel { get; set; }
        public List<TomadorBuscarLimitesCreditoSubLimiteCobertura> Coberturas { get; set; }
    }
}