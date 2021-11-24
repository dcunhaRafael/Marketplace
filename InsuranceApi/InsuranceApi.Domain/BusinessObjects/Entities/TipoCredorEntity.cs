using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public  class TipoCredorEntity
    {
        public int? CreditorTypeId { get; set; }
        public string Name { get; set; }
        public bool IsStateRequired { get; set; }
        public bool IsLocalityRequired { get; set; }
        public bool IsCreditorDataRequired { get; set; }
        public string ExternalCode { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }

    }
}
