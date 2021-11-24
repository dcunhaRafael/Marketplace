using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class LegalRecourseTypeParameterEntity {
        public int Id { get; set; }
        public int LegalRecourseTypeId { get; set; }
        public decimal ApeelDepositAmount { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
