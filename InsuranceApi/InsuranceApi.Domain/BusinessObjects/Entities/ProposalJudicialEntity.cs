namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalJudicialEntity {

        public string CodigoProduto { get; set; }
        public string CodigoModalidade { get; set; }
        public int DiasPrazoVigencia { get; set; }
        public decimal ValorGarantia { get; set; }
        public string NumeroContrato { get; set; }
        public string CpfCnpjTomador { get; set; }
        public InsuredEntity Segurado { get; set; }
    }
}
