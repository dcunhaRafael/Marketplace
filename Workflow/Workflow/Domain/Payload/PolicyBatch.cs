using Domain.Enumerators;
using System;

namespace Domain.Payload {
    public class PolicyBatch {
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
        public RecordStatusEnum? Status { get; set; }
        public int LoggedUserId { get; set; }

        public Exception Exception { get; set; }
    }
}
