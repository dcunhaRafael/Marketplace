using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalRenewalEntity {
        public long? PolicyCode { get; set; }
        //public int? ProductExternalId { get; set; }
        //public int? CoverageExternalId { get; set; }
        //public DateTime? StartOfTerm { get; set; }
        //public DateTime? EndOfTerm { get; set; }
        //public int? BrokerExternalId { get; set; }
        public long? BrokerDocument { get; set; }
        //public int? InsuredExternalId { get; set; }
        //public int? TakerExternalId { get; set; }
        //public string InsuredObject { get; set; }
        //public decimal? InsuredAmount { get; set; }
        //public int? RenovationUpdateIndexId { get; set; }
        //public int? RenovationUpdateIndexBcCode { get; set; }
        public decimal? NewInsuredAmount { get; set; }
        //public decimal? NewPremiumValue { get; set; }
        public DateTime? NewStartOfTerm { get; set; }
        public DateTime? NewEndOfTerm { get; set; }
        //public int? NewProposalNumber { get; set; }
        //public int? NewProposalStatusId { get; set; }
        //public long? NewPolicyCode { get; set; }
        public string NewInsuredObject { get; set; }
        //public int? LastChangeUserId { get; set; }
        //public DateTime? LastChangeDate { get; set; }
        //public int? ProposalId { get; set; }
    }
}
