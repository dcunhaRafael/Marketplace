using Domain.Enumerators;
using System;

namespace Domain.Entities {
    public class ProposalInstallment {

        public ProposalInstallment() {

        }

        public int Id { get; set; }
        public int? ProposalId { get; set; }
        public int? Number { get; set; }
        public decimal? TariffPremium { get; set; }
        public decimal? CostValue { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TotalPremium { get; set; }
        public DateTime? DueDate { get; set; }
        public InstallmentStatus InstallmentStatus { get; set; }
    }
}
