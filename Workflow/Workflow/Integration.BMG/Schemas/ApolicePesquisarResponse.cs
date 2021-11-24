using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class ApolicePesquisarResponse : CodigoRetornoResponse {
        public List<ApolicePesquisarItem> Apolice_Pesquisar { get; set; }
    }

}