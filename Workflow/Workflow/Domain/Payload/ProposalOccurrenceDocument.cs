namespace Domain.Payload {
    public class ProposalOccurrenceDocument {
        public ProposalOccurrenceDocument() {
            this.DocumentType = new DocumentType();
        }

        public long? ProposalOccurrenceDocumentId { get; set; }
        public long ProposalOccurrenceId { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
    }
}
