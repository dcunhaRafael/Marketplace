using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public  class RateEntity {

        public int? AppealFeeId { get; set; }
        public decimal? InsuredAmountValueMin { get; set; }
        public decimal? InsuredAmountValueMax { get; set; }
        public int? TermTypeId { get; set; }
        public decimal? PremiumValue { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
