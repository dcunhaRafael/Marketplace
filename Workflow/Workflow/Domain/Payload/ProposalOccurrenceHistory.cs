using Domain.Enumerators;
using System;

namespace Domain.Payload {
    public class ProposalOccurrenceHistory {
        public ProposalOccurrenceHistory() {
            this.InclusionUser = new User();
        }

        public long ProposalOccurrenceHistoryId { get; set; }
        public long ProposalOccurrenceId { get; set; }
        public OccurrenceActionTypeEnum ActionType { get; set; }
        public string Description { get; set; }
        public User InclusionUser { get; set; }
        public DateTime InclusionDate { get; set; }
    }
}
