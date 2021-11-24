using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class ApoliceEmitirResponse : CodigoRetornoResponse {
        public string id_apolice { get; set; }
        public ApoliceEmitirItem Apolice { get; set; }
    }

}