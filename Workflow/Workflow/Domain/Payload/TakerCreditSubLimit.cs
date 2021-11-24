using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class TakerCreditSubLimit {

        public TakerCreditSubLimit() {
            this.Coverages = new List<TakerCreditSubLimitCoverage>();
        }

        public string CoverageGroupName { get; set; }
        public int GroupId { get; set; }
        public int TakerGroupId { get; set; }
        public decimal? SubLimitValue { get; set; }
        public decimal? AvailableSubLimitValue { get; set; }
        public IList<TakerCreditSubLimitCoverage> Coverages { get; set; }
    }
}
