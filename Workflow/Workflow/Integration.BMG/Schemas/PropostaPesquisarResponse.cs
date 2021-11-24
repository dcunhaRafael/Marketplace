using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class PropostaPesquisarResponse : CodigoRetornoResponse {
        public List<PropostaPesquisarItem> Proposta_Pesquisar { get; set; }
    }

}