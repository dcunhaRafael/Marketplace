using System;

namespace Domain.Payload {
    public class PolicyIssued {
        public long NumeroApolice { get; set; }
        public string CodigoStatus { get; set; }
        public string DescricaoStatus { get; set; }
    }
}
