using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class ProposalStatusEntity {
        public int Id { get; set; }      
        public string Name { get; set; }
        public string ExternalCode { get; set; }
        public int Status { get; set; }
    }
}
