using System;

namespace Domain.Payload {
    public class TakerCreditAssessment {

        public TakerCreditAssessment() {
            
        }

        public int Id { get; set; }
        public int TakerId { get; set; }
        public decimal? CreditLimitAmount { get; set; }
        public int? Score { get; set; }
        public string Rating { get; set; }
        public decimal? RiskRate { get; set; }
        public decimal? Quality { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
