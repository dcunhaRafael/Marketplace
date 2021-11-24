using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class TermTypeEntity {
        public int? TermSize { get; set; }
        public TermUnitEnum? TermUnit { get; set; }
        public int Id { get; set; }      
        public string Name { get; set; }
        public int Status { get; set; }
        public int CodigoExterno { get; set; }
        public int DiasPrazo { get; set; }
        public int UserId { get; set; }    
    }
}
