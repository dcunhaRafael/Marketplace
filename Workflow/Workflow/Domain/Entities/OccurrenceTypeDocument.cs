namespace Domain.Entities {
    public class OccurrenceTypeDocument : BaseEntity {
        public OccurrenceTypeDocument() {
            this.DocumentType = new DocumentType();
        }

        public int? OccurrenceTypeDocumentId { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public int? DocumentTypeId { get; set; }
        public bool IsRequired { get; set; }

        // Childs
        public DocumentType DocumentType { get; set; }
    }
}
