using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class AppAuditorshipEntity {
        public long AppAuditorshipId { get; set; }
        public string EntityClass { get; set; }
        public long EntityId { get; set; }
        public string ActionName { get; set; }
        public string RecordData { get; set; }
        public int? UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
