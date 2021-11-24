using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class ProdutoBuscarRequest {
        public int? cd_produto { get; set; }
        public int id_usuario { get; set; }
        public string cd_susep { get; set; }
        public bool dv_habilitado_usuario { get; set; }
        public bool dv_habilitado_emissao_portal { get; set; }
        public bool dv_vigente_portal { get; set; }
    }

    public class ProdutoBuscarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<ProdutoBuscarItem> Produto_Buscar { get; set; }
    }

    public class ProdutoBuscarItem {
        public int cd_produto { get; set; }
        public string nm_produto { get; set; }
    }
}
