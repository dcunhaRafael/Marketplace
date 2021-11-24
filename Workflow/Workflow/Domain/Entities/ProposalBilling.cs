using System;

namespace Domain.Entities {
    public class ProposalBilling {

        public ProposalBilling() {
            PaymentInstallment = new PaymentInstallment();
            PaymentFrequency = new PaymentFrequency();
            PaymentMethod = new PaymentMethod();
            PaymentMethodOthers = new PaymentMethod();
        }

        public int Id { get; set; }
        public PaymentInstallment PaymentInstallment { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime? DueDate { get; set; }
        public PaymentMethod PaymentMethodOthers { get; set; }
        public int? PaymentDay { get; set; }
    }
}
