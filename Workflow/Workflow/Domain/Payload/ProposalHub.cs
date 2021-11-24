using System;

namespace Domain.Payload {
    public class ProposalHub {
        public ProposalHub() {
            this.ProposalType = new ProposalType();
            this.Product = new Product();
            this.Coverage = new Coverage();
            this.Broker = new Broker();
            this.Taker = new Taker();
            this.Insured = new Insured();
        }

        public int? ProposalId { get; set; }
        public int? ProposalNumber { get; set; }
        public DateTime? ProposalDate { get; set; }
        public DateTime? EffectiveDateStart { get; set; }
        public DateTime? EffectiveDateEnd { get; set; }
        public ProposalType ProposalType { get; set; }
        public Product Product { get; set; }
        public Coverage Coverage { get; set; }
        public Broker Broker { get; set; }
        public Taker Taker { get; set; }
        public Insured Insured { get; set; }
    }
}
