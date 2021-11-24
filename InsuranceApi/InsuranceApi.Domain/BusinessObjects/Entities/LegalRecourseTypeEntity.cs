using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class LegalRecourseTypeEntity {

        public int? LegalRecourseTypeId { get; set; }
        public string Name { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int? ExternalCode { get; set; }
        public int? UserId { get; set; }
        public DateTime DateUtc { get; set; }

    }
}
