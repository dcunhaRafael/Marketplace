using Domain.Enumerators;
using System;

namespace Domain.Entities {
    public class PolicyRenovation {
        public int? PolicyRenovationId { get; set; }
        public int? PolicyBatchId { get; set; }
        public long? PolicyCode { get; set; }
        public int? EndorsementId { get; set; }
        public int? ProductExternalId { get; set; }
        public string ProductName { get; set; }
        public int? CoverageExternalId { get; set; }
        public string CoverageName { get; set; }
        public DateTime? StartOfTerm { get; set; }
        public DateTime? EndOfTerm { get; set; }
        public int? BrokerExternalId { get; set; }
        public long? BrokerDocument { get; set; }
        public string BrokerName { get; set; }
        public int? InsuredExternalId { get; set; }
        public string InsuredName { get; set; }
        public int? TakerExternalId { get; set; }
        public long? TakerDocument { get; set; }
        public string TakerName { get; set; }
        public decimal? TakerCreditLimit { get; set; }
        public string TakerRating { get; set; }
        public decimal? TakerRiskRate { get; set; }
        public string InsuredObject { get; set; }
        public decimal? InsuredAmount { get; set; }
        public int? RenovationUpdateIndexId { get; set; }
        public int? RenovationUpdateIndexBcCode { get; set; }
        public decimal? NewInsuredAmount { get; set; }
        public decimal? NewPremiumValue { get; set; }
        public DateTime? NewStartOfTerm { get; set; }
        public DateTime? NewEndOfTerm { get; set; }
        public int? NewProposalNumber { get; set; }
        public int? NewProposalStatusId { get; set; }
        public string NewProposalStatusName { get; set; }
        public long? NewPolicyCode { get; set; }
        public string NewInsuredObject { get; set; }
        public int? LastChangeUserId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public RenovationStatusEnum? RenovationStatusId { get; set; }
        public int? ProposalId { get; set; }
    }
}
