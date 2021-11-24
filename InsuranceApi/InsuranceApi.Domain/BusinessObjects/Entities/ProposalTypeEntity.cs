using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class ProposalTypeEntity {
        public int Id { get; set; }      
        public string Name { get; set; }
        public string ProductExternalCode { get; set; }
        public string CoverageExternalCode { get; set; }
    }
}
