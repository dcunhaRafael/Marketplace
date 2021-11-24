using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProfileEntity {
        public int? ProfileId { get; set; }
        public string Description { get; set; }
        public bool IsProducerProfile { get; set; }
        public int? MenuId { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
