using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class TipoTributoEntity
    {
        public int? TributeTypeId { get; set; }
        public string Name { get; set; }
        public string ExternalCode { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
