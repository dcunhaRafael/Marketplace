using System;

namespace Domain.Entities {
    public class ProposalWarranty {

        public ProposalWarranty() {
            Currency = new Currency();
            InsuredObject = new InsuredObject();
        }

        public int Id { get; set; }
        public decimal? InsuredAmount { get; set; }
        public decimal? RiskRate { get; set; }
        public Currency Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? IofPercentage { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TariffPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? ComissionPercentage { get; set; }
        public decimal? ComissionValue { get; set; }
        public InsuredObject InsuredObject { get; set; }
        public string InsuredObjectText { get; set; }
    }
}
