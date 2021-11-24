using Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace Domain.Entities {
    public class ProposalOccurrence : BaseEntity {
        public ProposalOccurrence() {
            this.Proposal = new Proposal();
            this.OccurrenceType = new OccurrenceType();
            this.RefusalReason = new RefusalReason();
            this.InclusionUser = new User();
            this.Documents = new List<ProposalOccurrenceDocument>();
            this.Histories = new List<ProposalOccurrenceHistory>();
        }

        public long? ProposalOccurrenceId { get; set; }
        public int? ProposalId { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public OccurrenceStatusEnum? OccurrenceStatus { get; set; }
        public DateTime? ApprovalRefusalDate { get; set; }
        public int? RefusalReasonId { get; set; }
        public string UserComments { get; set; }

        // Others
        public int DocumentTypeCount { get; set; }
        public int DocumentTypePendingCount { get; set; }

        // Childs
        public Proposal Proposal { get; set; }
        public OccurrenceType OccurrenceType { get; set; }
        public RefusalReason RefusalReason { get; set; }
        public IList<ProposalOccurrenceDocument> Documents { get; set; }
        public IList<ProposalOccurrenceHistory> Histories { get; set; }
    }
}
