namespace Domain.Entities {
    public class RefusalReason : BaseEntity {
        public int? RefusalReasonId { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
