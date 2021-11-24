namespace Domain.Entities {
    public class DocumentType: BaseEntity {
        public int? DocumentTypeId { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
