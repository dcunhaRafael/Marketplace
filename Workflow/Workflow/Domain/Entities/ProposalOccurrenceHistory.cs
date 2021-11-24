using Domain.Enumerators;
using System;

namespace Domain.Entities {
    public class ProposalOccurrenceHistory {
        public ProposalOccurrenceHistory() {
            this.InclusionUser = new User();
        }

        public long? ProposalOccurrenceHistoryId { get; set; }
        public long? ProposalOccurrenceId { get; set; }
        public OccurrenceActionTypeEnum ActionType { get; set; }
        public string Description { get; set; }
        public int InclusionUserId { get; set; }
        public DateTime InclusionDate { get; set; }

        // Childs
        public User InclusionUser { get; set; }
    }
}
