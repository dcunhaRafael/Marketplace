using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class WarrantyOptionsEntity {

        public int Id { get; set; }
        public int ProposalId { get; set; }
        public string RecursalModalityType { get; set; }
        public decimal RecursalModalityMaxValue { get; set; }
        public decimal RecursalModalityDepositValue { get; set; }
        public decimal RecursalModalityPercentageHarm { get; set; }
        public decimal RecursalModalityAmountOfInsuredValue { get; set; }
        public int DeadLineValidityOption { get; set; }
        public DateTime DeadLineValidity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedByThatUserId { get; set; }
    }
}
