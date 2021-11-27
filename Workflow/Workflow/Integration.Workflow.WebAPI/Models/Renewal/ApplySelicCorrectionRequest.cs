using System;

namespace Integration.Workflow.WebAPI.Models.Renewal {
    public class ApplySelicCorrectionRequest {
        public decimal InsuredAmount { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime EndOfTerm { get; set; }
    }
}
