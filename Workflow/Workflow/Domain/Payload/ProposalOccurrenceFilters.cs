using Domain.Enumerators;
using System;

namespace Domain.Payload {
    public class ProposalOccurrenceFilters {
        public long? ProposalOccurrenceId { get; set; }
        public int? ProposalNumber { get; set; }
        public int? ProductId { get; set; }
        public int? CoverageId { get; set; }
        public OccurrenceStatusEnum? OccurrenceStatus { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public int? BrokerId { get; set; }
        public int? InsuredId { get; set; }
        public int? TakerId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? LoggedUserId { get; set; }
        public int? LoggedUserProfileId { get; set; }
    }
}
