using System;

namespace Domain.Payload {
    public class LatePaymentSlip {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string BrokerName { get; set; }
        public string BrokerSusepCode { get; set; }
        public string BrokerLegacyCode { get; set; }
        public long? BrokerDocument { get; set; }
        public string TakerName { get; set; }
        public long? TakerDocument { get; set; }
        public PersonAddress TakerAddress { get; set; }
        public string InsuredName { get; set; }
        public long? InsuredDocument { get; set; }
        public PersonAddress InsuredAddress { get; set; }
        public long? PolicyNumber { get; set; }
        public int? ProposalNumber { get; set; }
        public int? EndorsementNumber { get; set; }
        public int? EndorsementId { get; set; }
        public int? InstallmentNumber { get; set; }
        public int? InstallmentCount { get; set; }

        public string PaymentMethodName { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TariffPremiumValue { get; set; }
        public decimal? NetPremiumValue { get; set; }
        public decimal? AdditionalFractionation { get; set; }
        public decimal? CostValue { get; set; }
        public decimal? IofValue { get; set; }
        public decimal? TotalPremiumValue { get; set; }
        public int? LateDays { get; set; }
        public string OurNumber { get; set; }

        public DateTime? NewDueDate { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? InterestPercent { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? NewTotalValue { get; set; }
        public int? NewLateDays { get; set; }
        public int? NewOurNumber { get; set; }

        public string BarcodeNumber { get; set; }
        public string BarcodeDigitableLine { get; set; }

        public int? BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}
