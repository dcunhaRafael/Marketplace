using System.Collections.Generic;

namespace Domain.Payload {
    public class ProposalValidation {
        public ProposalValidation() {
            this.Restrictions = new List<ProposalRestrictionCard>();
        }

        public bool IsPending { get; set; }
        public bool IsRefused { get; set; }
        public bool IsApproved { get; set; }
        public IList<ProposalRestrictionCard> Restrictions { get; set; }
    }
}
