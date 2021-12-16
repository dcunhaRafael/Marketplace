using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Portal.Web.Models.Payment {
    public class LatePaymentSlipViewModel {

        public LatePaymentSlipViewModel() {
            this.Products = new List<Product>();
            this.SummaryByValue = new List<LatePaymentSlipSummary>();
            this.SummaryByCount = new List<LatePaymentSlipSummary>();
            this.Results = new List<LatePaymentSlip>();
            this.CurrentItem = new LatePaymentSlip();
        }

        public IList<Product> Products { get; set; }

        public int? BrokerUserId { get; set; }
        public string BrokerSusepCode { get; set; }
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

        public IList<LatePaymentSlipSummary> SummaryByValue { get; set; }
        public IList<LatePaymentSlipSummary> SummaryByCount { get; set; }

        public IList<LatePaymentSlip> Results { get; set; }
        public LatePaymentSlip CurrentItem { get; set; }
    }
}
