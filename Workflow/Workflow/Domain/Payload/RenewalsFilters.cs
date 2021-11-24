using Domain.Enumerators;

namespace Domain.Payload {
    public class RenewalsFilters {
        public int? PolicyBatchId { get; set; }
        public OccurrenceStatusEnum? BatchType { get; set; }
        public string Competency { get; set; }
        public OccurrenceStatusEnum? BatchStatus { get; set; }
        //public int? ProposalNumber { get; set; }
        //public long? PolicyNumber { get; set; }
        public int? BrokerId { get; set; }
        public int? BrokerExternalId { get; set; }
        public int? TakerId { get; set; }
        public int? TakerExternalId { get; set; }
        public int? InsuredId { get; set; }
        public int? InsuredExternalId { get; set; }
        public int? LoggedUserId { get; set; }
    }
}
