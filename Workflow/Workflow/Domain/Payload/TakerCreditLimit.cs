
using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class TakerCreditLimit {
        public TakerCreditLimit() {
            this.Sublimits = new List<TakerCreditSubLimit>();
        }

        public string Name { get; set; }
        public long? CpfCnpjNumber { get; set; }
        public string LegacyCode { get; set; }
        public decimal? AvailableCreditLimit { get; set; }
        public decimal? AvailableCreditLimitReinsurance { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? ComplianceValidityDate { get; set; }
        public IList<TakerCreditSubLimit> Sublimits { get; set; }
        public IList<TakerCreditSubLimitParentGroup> TakersParentsGroup { get; set; }
    }
}
