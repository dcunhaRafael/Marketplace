using System.Collections.Generic;

namespace Domain.Payload {
    public class AnalyzeProposal {
        public AnalyzeProposal() {
            this.Occurrences = new List<ProposalOccurrence>();
        }

        public bool IsRefused { get; set; }
        public bool IsApproved { get; set; }
        public IList<ProposalOccurrence> Occurrences { get; set; }
    }
}
