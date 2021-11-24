namespace Domain.Entities {
    public class Product : BaseEntity {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
