using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class AssinaturaTomadorEntity {
        public long? CnpjTomador { get; set; }
        public string RazaoSocialTomador { get; set; }
        public long? CpfRepresentanteLegal { get; set; }
        public string NomeRepresentanteLegal { get; set; }
        public string EmailRepresentanteLegal { get; set; }
        public DateTime? NascimentoRepresentanteLegal { get; set; }
        public int IdTomador { get; set; }
        public DocumentEntity Impresso { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string OrigemEmpresa { get; set; }
    }
}
