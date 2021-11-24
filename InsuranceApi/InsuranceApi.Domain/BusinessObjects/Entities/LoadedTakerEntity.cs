using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class LoadedTakerEntity : BaseEntityAuditorship {

        public LoadedTakerEntity() {
            LastTransmission = new LoadedTakerSignatureEntity();
        }

        public int LoadedTakerRecordId { get; set; }
        public int? UploadedFileId { get; set; }
        public int? FileLineIndex { get; set; }
        public StatusRegistroTomadorEnum RecordStatus { get; set; }
        public string JsonRecord { get; set; }
        public string JsonValidation { get; set; }
        public TipoPessoaEnum TakerType { get; set; }
        public string TakerName { get; set; }
        public long? TakerDocumentNumber { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? ZipCode { get; set; }
        public string Rating { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Limit { get; set; }
        public string ResponseStatus { get; set; }
        public string SystemStatus { get; set; }

        public DateTime? LoadedDate { get; set; }
        public StatusCargaTomadorEnum Status { get; set; }
        public int? LegacyId { get; set; }
        public int? LegacyUserId { get; set; }

        public decimal? HigherPresumedBilling { get; set; }
        public int? Score { get; set; }
        public string Telephone { get; set; }
        public decimal? ZipCodeAssumedInvoicingAmount { get; set; }
        public decimal? AssumedInvoicingAmountSerasa { get; set; }

        public LoadedTakerSignatureEntity LastTransmission { get; set; }
    }
}
