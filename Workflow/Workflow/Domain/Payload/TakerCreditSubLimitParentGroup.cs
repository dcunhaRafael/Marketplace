using System;

namespace Domain.Payload {
    public class TakerCreditSubLimitParentGroup {
        public string Name { get; set; }
        public long CpfCnpjNumber { get; set; }
        public int LegacyCode { get; set; }
        public decimal? ParticipationPercentage { get; set; }
        public bool GenerateAccumulation { get; set; }
        public decimal? AvailableCreditLimit { get; set; }
        public decimal? AvailableCreditLimitReinsurance { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? ComplianceValidityDate { get; set; }
    }
}