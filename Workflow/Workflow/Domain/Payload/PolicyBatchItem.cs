using System;

namespace Domain.Payload {
    public class PolicyBatchItem {
        public int? EndorsementId { get; set; }
        public string PolicyCode { get; set; }
        public string SourceTypeName { get; set; }
        public int? EndorsementNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? StartOfTerm { get; set; }
        public DateTime? EndOfTerm { get; set; }
        public decimal? InsuredAmount { get; set; }
        public int? BrokerExternalId { get; set; }
        public string BrokerName { get; set; }
        public long? BrokerDocument { get; set; }
        public string BrokerEmails { get; set; }
        public int? InsuredExternalId { get; set; }
        public string InsuredName { get; set; }
        public long? InsuredDocument { get; set; }
        public string InsuredEmails { get; set; }
        public int? ProductExternalId { get; set; }
        public string ProductName { get; set; }
        public int? CoverageExternalId { get; set; }
        public string CoverageName { get; set; }
        public int? ExpirationDays { get; set; }
        public int? TakerExternalId { get; set; }
        public string TakerName { get; set; }
        public long? TakerDocument { get; set; }
        public string TakerEmails { get; set; }
        public int? RenovationUpdateIndexId { get; set; }
        public DateTime? ProcessingDate { get; set; }
        public int? PolicyBatchId { get; set; }
    }
}
