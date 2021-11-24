using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class LaborCourtEntity: BaseEntityAuditorship {

        public int? LaborCourtId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? ZipCode { get; set; }
        public string ZipCodeString { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int ExternalCode { get; set; }
        public string ExternalPersonCode { get; set; }
        public string ExternalAddressCode { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
