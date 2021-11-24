using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class LoadedTakerSignatureEntity {

        public int LoadedTakerSignatureId { get; set; }
        public int LoadedTakerRecordId { get; set; }
        public long? LegalRepresentativeDocumentNumber { get; set; }
        public string LegalRepresentativeName { get; set; }
        public DateTime? LegalRepresentativeBornIn { get; set; }
        public string LegalRepresentativeMail { get; set; }
        public int? SignatureId { get; set; }
        public DateTime? SignatureDate { get; set; }
        public string SignedDocument { get; set; }
        public string SignedDocumentName { get; set; }
        public DateTime? DateUtc { get; set; }
        public int? UserId { get; set; }
    }
}
