using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public   class ProposalAssinaturaEntity {
        public int CodigoProposta { get; set; }
        public int IdTransacao { get; set; }
        public string DocumentoAssinatura { get; set; }
        public string NomeDocumentoAssinatura { get; set; }
        public DateTime DataAssinatura { get; set; }
    }
}
