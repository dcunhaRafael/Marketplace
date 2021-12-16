using System;

namespace Integration.Workflow.WebAPI.Models.Payment {
    public class ListLatePaymentSlipRequest {
        public string BrokerLegacyCode { get; set; }
        public string TakerLegacyCode { get; set; }
        public string InsuredLegacyCode { get; set; }
        public string ProductLegacyCode { get; set; }
        public long? PolicyNumber { get; set; }
        public int? EndorsementNumber { get; set; }
        public int? InstallmentNumber { get; set; }
        public decimal? PremiumValue { get; set; }
        public string OurNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
