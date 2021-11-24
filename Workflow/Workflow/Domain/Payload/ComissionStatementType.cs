
using Domain.Entities;

namespace Domain.Payload {
    public class ComissionStatementType {
        public ComissionStatementType() {
            this.Broker = new Broker();
        }

        public Broker Broker { get; set; }
        public int ComissionTypeId { get; set; }
        public string ComissionTypeName { get; set; }
        public decimal? ComissionValue { get; set; }

    }
}
