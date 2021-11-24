namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class IssueProposalEntity {

        public IssueProposalEntity() {
            PropostaAssinada = new ProposalSignatureEntity();
        }        
        public int CodigoProposta { get; set; }
        public ProposalSignatureEntity PropostaAssinada { get; set; }
    }
}
