using Domain.Enumerators;

namespace Domain.Payload {
    public class OccurrenceTypeFilters {
        public int? OccurrenceTypeId { get; set; }
        public int? ProductId { get; set; }
        public int? CoverageId { get; set; }
        public string OccurrenceCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OccurrenceTypeEnum? Type { get; set; }
        public ValidationRuleEnum? ValidationRule { get; set; }
        public bool? IsTransmissionLocked { get; set; }
        public bool? IsIssuanceLocked { get; set; }
        public RequiredActionEnum? RequiredAction { get; set; }
        public bool? AutomaticRefusal { get; set; }
        public int? ProfileId { get; set; }
        public RecordStatusEnum? Status { get; set; }
    }
}
