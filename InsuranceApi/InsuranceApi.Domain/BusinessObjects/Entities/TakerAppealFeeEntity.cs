using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class TakerAppealFeeEntity {

        public int? TakerAppealFeeId { get; set; }
        public int? TakerId { get; set; }
        public int? ProductId { get; set; }
        public int? CoverageId { get; set; }
        public decimal? InsuredAmountValueMin { get; set; }
        public decimal? InsuredAmountValueMax { get; set; }
        public int? TermTypeId { get; set; }
        public decimal? PremiumValue { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }

        public int? TakerExternalId { get; set; }
        public string TakerName { get; set; }
        public long? TakerDocument { get; set; }
        public string ProductName { get; set; }
        public string CoverageName { get; set; }
        public string TermTypeName { get; set; }
    }
}
