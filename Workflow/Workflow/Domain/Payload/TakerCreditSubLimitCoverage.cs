using System;

namespace Domain.Payload {
    public class TakerCreditSubLimitCoverage {

        public TakerCreditSubLimitCoverage() {
            this.Coverage = new Coverage();
            this.Product = new Product();
        }

        public int SubLimitCoverageGroupId { get; set; }
        public Coverage Coverage { get; set; }
        public Product Product { get; set; }
    }
}
