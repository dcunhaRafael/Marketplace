using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class StateEntity
    {
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string Initials { get; set; }
        public string ExternalCode { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
