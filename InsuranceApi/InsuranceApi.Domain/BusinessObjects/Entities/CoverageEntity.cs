using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class CoverageEntity {
        public int? IdCobertura { get; set; }

        public string NomeCobertura { get; set; }
        public string Description { get; set; }
        public int? ExternalCode { get; set; }
        public InsuredInputTypeEnum? InsuredInputType { get; set; }
        public LawsuitInputTypeEnum? LawsuitInputType { get; set; }
        public bool DisplayRequesterInformation { get; set; }
        public bool DisplayLawsuitTypeInformation { get; set; }
        public int? DefaultPaymentFormId { get; set; }
        public int? DefaultPaymentInstallmentId { get; set; }
        public int? DefaultPaymentFrequencyId { get; set; }
        public int? DefaultTermTypeId { get; set; }
        public PaymentDayEnum? DefaultPaymentDay { get; set; }
        public bool IsBillingDataLocked { get; set; }
        public RecordStatusEnum? Status { get; set; }        
    }
}
