using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class TipoProcessoEntity
    {
        public int? LawsuitTypeId { get; set; }
        public string Name { get; set; }
        public string ExternalCode { get; set; }
        public bool? IsComplementRequired { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
