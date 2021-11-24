using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
   public  class AssinaturaConsultarEntity {
        public int IdTransacao { get; set; }
        public long? CpfCnpj { get; set; }
        public string Nome { get; set; }
        public int CodigoProposta { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public ImpressoEntity ImpressoOriginal { get; set; }
        public ImpressoEntity ImpressoAssinado { get; set; }
        public DateTime TimestampInclusao { get; set; }
    }

    public class ImpressoEntity {
        public string Href { get; set; }

    }
}
