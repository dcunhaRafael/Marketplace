using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ClaimantsEntity {

        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int ProposalId1 { get; set; }
        public int InsuredId { get; set; }
        public bool IsTheMajor { get; set; }
        public bool IsNewInsured { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedByThatUserId { get; set; }
    }
}
