using Domain.Enumerators;
using System.Collections.Generic;

namespace Domain.Payload {
    public class OccurrenceType {
        public OccurrenceType() {
            this.Product = new Product();
            this.Coverage = new Coverage();
            this.Profile = new Profile();
            this.LiberationUsers = new List<OccurrenceTypeLiberationUser>();
            this.Documents = new List<OccurrenceTypeDocument>();
        }

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
        public int? NormalSignalingTimeout { get; set; }
        public int? WarningSignalingTimeout { get; set; }
        public int? CriticalSignalingTimeout { get; set; }
        public Product Product { get; set; }
        public Coverage Coverage { get; set; }
        public Profile Profile { get; set; }
        public IList<OccurrenceTypeLiberationUser> LiberationUsers { get; set; }
        public IList<OccurrenceTypeDocument> Documents { get; set; }
        public RecordStatusEnum Status { get; set; }
        public int? LoggedUserId { get; set; }
    }
}
