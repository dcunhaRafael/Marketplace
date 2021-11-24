using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarLimitesCreditoSubLimiteCobertura {
        public string nm_comercial { get; set; }
        public int id_grp_sub_limite_cobertura { get; set; }
        public int id_produto_cobertura { get; set; }
        public string nm_produto { get; set; }
        public int cd_produto { get; set; }
    }
}