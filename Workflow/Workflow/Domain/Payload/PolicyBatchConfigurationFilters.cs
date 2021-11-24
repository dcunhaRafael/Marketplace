using Domain.Enumerators;

namespace Domain.Payload {
    public class PolicyBatchConfigurationFilters {
        public int? BatchType { get; set; }
        public RecordStatusEnum? Status { get; set; }
    }
}
