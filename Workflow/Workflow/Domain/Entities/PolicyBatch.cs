using Domain.Enumerators;

namespace Domain.Entities {
    public class PolicyBatch : BaseEntity {
        public PolicyBatch() {

        }

        public int? PolicyBatchId { get; set; }
        public PolicyBatchRenovationEnum? BatchType { get; set; }
        public int? BrokerExternalId { get; set; }
        public int? TakerExternalId { get; set; }
        public int? InsuredExternalId { get; set; }
        public string Competency { get; set; }
        public int PolicyCount { get; set; }
        public decimal PolicyTotal { get; set; }
        public PolicyBatchStatusEnum? BatchStatus { get; set; }
    }
}
