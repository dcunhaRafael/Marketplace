namespace Domain.Entities {
    public class ProposalOccurrenceDocument : BaseEntity {
        public ProposalOccurrenceDocument() {
            this.DocumentType = new DocumentType();
            this.InclusionUser = new User();
        }

        public long ProposalOccurrenceDocumentId { get; set; }
        public long ProposalOccurrenceId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }

        // Childs
        public DocumentType DocumentType { get; set; }
    }
}
