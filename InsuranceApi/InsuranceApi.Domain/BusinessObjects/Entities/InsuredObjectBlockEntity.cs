using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class InsuredObjectBlockEntity {

        public int? InsuredObjectBlockId { get; set; }
        public int? InsuredObjectId { get; set; }
        public InsuredObjectPrintModeEnum? PrintMode { get; set; }
        public string Contents { get; set; }
        public bool StartInNewLine { get; set; }
        public int PrintOrder { get; set; }
        public RecordStatusEnum? Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
