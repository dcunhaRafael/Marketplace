using System;

namespace Domain.Entities {
    public class ProposalPremium {
        public string InsurerImage { get; set; }
        public int? ProposalNumber { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? IofPercentage { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TariffPremium { get; set; }
        public decimal? ComissionPercentage { get; set; }
        public decimal? ComissionValue { get; set; }
        public decimal? RiskRate { get; set; }
        public string Message { get; set; }
    }
}
