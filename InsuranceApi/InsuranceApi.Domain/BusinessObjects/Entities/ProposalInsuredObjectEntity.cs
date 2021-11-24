using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public  class ProposalInsuredObjectEntity {

        public int Id { get; set; }
        public int ProposalTypeId { get; set; }
        public string Text { get; set; }
    }
}
