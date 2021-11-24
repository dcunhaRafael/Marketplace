namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalSignatureEntity {

        public ProposalSignatureEntity() {
            Documento = new DocumentEntity();
        }        
        public string IP { get; set; }
        public string UserAgent { get; set; }        
        public DocumentEntity Documento { get; set; }
    }
}
