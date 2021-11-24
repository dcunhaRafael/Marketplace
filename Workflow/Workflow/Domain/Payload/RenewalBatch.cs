using Domain.Enumerators;

namespace Domain.Payload {
    public class RenewalBatch {
        public RenewalBatch() {

        }

        public int? BatchNumber { get; set; }
        public RenewalBatchTypeEnum? BatchType { get; set; }
        public string Competence { get; set; }
        public int? ProposalCount { get; set; }
        public decimal? TotalInsuredAmount { get; set; }
        public RenewalBatchStatusEnum? BatchStatus { get; set; }
    }
}
