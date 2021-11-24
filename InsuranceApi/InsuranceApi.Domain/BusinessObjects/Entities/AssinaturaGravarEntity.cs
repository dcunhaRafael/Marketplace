namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public  class AssinaturaGravarEntity {

        public long? CpfCnpj { get; set; }
        public string Nome { get; set; }
        public int CodigoProposta { get; set; }        
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string OrigemEmpresa { get; set; }
        public DocumentEntity Documento { get; set; }
    }
}
