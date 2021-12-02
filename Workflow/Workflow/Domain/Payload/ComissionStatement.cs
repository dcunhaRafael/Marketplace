using Domain.Entities;
using System;

namespace Domain.Payload {
    public class ComissionStatement {
        public ComissionStatement() {
            this.Broker = new Broker();
        }

        public int StatementNumber { get; set; }

        public Broker Broker { get; set; }
        //public int BrokerLegacyCode { get; set; }
        //public string BrokerSusepCode { get; set; }
        //public string BrokerName { get; set; }
        //public int BrokerUserId { get; set; }

        public string Competency { get; set; }
        public int EntryCount { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public decimal? ComissionValue { get; set; }
        public DateTime? PayDay { get; set; }
        public string StatusName { get; set; }
        public string StatusBackgroundColor { get; set; }
        public string StatusTextColor { get; set; }
    }
}
