
namespace Domain.Payload {
    public class ProposalWarranty {
        public Coverage Coverage { get; set; }
        public decimal? InsuredAmount { get; set; }
        public decimal? RiskRate { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? IofPercentage { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TariffPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? ComissionPercentage { get; set; }
        public decimal? ComissionValue { get; set; }
    }
}
