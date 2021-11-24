using Domain.Payload;
using System.Collections.Generic;

namespace Integration.Workflow.WebAPI.Models.Proposal {
    public class ValidateProposalResponse {
        public ValidateProposalResponse() {
            this.Occurrences = new List<ProposalOccurrence>();
        }

        public bool IsRefused { get; set; }
        public bool IsApproved { get; set; }
        public IList<ProposalOccurrence> Occurrences { get; set; }
    }
}
