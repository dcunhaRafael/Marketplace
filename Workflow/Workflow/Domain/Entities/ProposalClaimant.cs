using System;

namespace Domain.Entities {
    public class ProposalClaimant {

        public ProposalClaimant() {

        }

        public int Id { get; set; }
        public int? ProposalId { get; set; }
        public Person Person { get; set; }
        public bool IsMainClaimant { get; set; }
    }
}
