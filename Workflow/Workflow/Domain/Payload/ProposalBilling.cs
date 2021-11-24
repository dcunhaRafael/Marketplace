using System;

namespace Domain.Payload {
    public class ProposalBilling {
        public DateTime? DueDateFirst { get; set; }
        public int? BillingDay { get; set; }
        public ProductPaymentMethod PaymentMethodFirst { get; set; }
        public ProductPaymentMethod PaymentMethodOthers { get; set; }
        public ProductPaymentInstallment PaymentInstallment { get; set; }
        public ProductPaymentFrequency PaymentFrequency { get; set; }
    }
}
