using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class ComissionStatementDetail {
        public ComissionStatementDetail() {
            this.Broker = new Broker();
            this.Taxes = new List<ComissionStatementDetailTax>();
        }

        public Broker Broker { get; set; }
        public int StatementNumber { get; set; }
        public string Competency { get; set; }
        public int EntryCount { get; set; }
        public decimal? ComissionValue { get; set; }
        public DateTime? PayDay { get; set; }
        public DateTime? PaymentRequestDate { get; set; }
        public string StatusName { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TaxableComissionValue { get; set; }
        public decimal? NotTaxableComissionValue { get; set; }
        public int ReceiptNumber { get; set; }
        public IList<ComissionStatementDetailTax> Taxes { get; set; }

        public decimal? ComissionNetValue {
            get {
                if (this.ComissionValue != null) {
                    var netValue = this.ComissionValue.Value;
                    netValue = (netValue - (this.TaxValue ?? 0));
                    return netValue;
                }
                return null;
            }
        }

        public string ImportantWarningText { get; set; }
        public string PaymentBank { get; set; }
        public string PaymentBranch { get; set; }
        public string PaymentAccount { get; set; }
    }
}
