using Domain.Entities;

namespace Domain.Payload {
    public class ComissionStatementBusiness {
        public ComissionStatementBusiness() {
            this.Broker = new Broker();
        }

        public Broker Broker { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public int ComissionTypeId { get; set; }
        public string ComissionTypeName { get; set; }
        public decimal? ComissionValue { get; set; }

    }
}
