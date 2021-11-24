using Domain.Entities;

namespace Domain.Payload {
    public class ComissionStatementSummary {
        public ComissionStatementSummary() {
        }

        public ComissionStatementStatus Status { get; set; }
        public decimal Value { get; set; }
        public decimal Percentage { get; set; }
    }
}
