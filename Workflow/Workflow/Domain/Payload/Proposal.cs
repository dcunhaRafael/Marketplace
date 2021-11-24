using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class Proposal {
        public int? PolicyId { get; set; }
        public int? EndorsementId { get; set; }
        public string PolicyCode { get; set; }
        public DateTime? ProposalDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? StartOfTerm { get; set; }
        public DateTime? EndOfTerm { get; set; }
        public ProposalStatus Status { get; set; }
        public int? ProposalNumber { get; set; }
        public string InsuredObjectText { get; set; }
        public string AttachedLaborText { get; set; }
        public bool? IsPremiumInformed { get; set; }
        public int? InclusionBrokerUserId { get; set; }
        public int? ChangeBrokerUserId { get; set; }

        public Product Product { get; set; }
        public Broker Broker { get; set; }
        public Taker Taker { get; set; }
        public Insured Insured { get; set; }

        public ProposalBilling BillingData { get; set; }
        public ProposalWarranty GuaranteeData { get; set; }
        public IList<ProposalInstallment> Installments { get; set; }
    }
}
