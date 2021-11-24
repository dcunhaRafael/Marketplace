namespace Domain.Payload {
    public class DocumentType {
        public int? OccurrenceTypeDocumentId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool IsChecked { get; set; }
    }
}
