using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class ProposalInstallment {
        public int? Number { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TariffPremium { get; set; }
        public decimal? CostValue { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TotalPremium { get; set; }
        public bool IsInstallmentPaid { get; set; }
    }
}
