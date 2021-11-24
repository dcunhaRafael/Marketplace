using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities
{
    public class MunicipioEntity
    {
        public long? LocalityId { get; set; }
        public int? StateId { get; set; }
        public LocalidadeEnum? LocalityType { get; set; }
        public string ExternalCode { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string ZipCodeLocality { get; set; }
        public string IbgeCode { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }

        // Auxiliar
        public string StateName { get; set; }
        public bool IsChecked { get; set; }
    }
}
