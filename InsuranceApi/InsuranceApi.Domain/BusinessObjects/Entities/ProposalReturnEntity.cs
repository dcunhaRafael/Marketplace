namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalReturnEntity {
        public ProposalEntity Proposta { get; set; }
        public bool Success { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }
    }
}
