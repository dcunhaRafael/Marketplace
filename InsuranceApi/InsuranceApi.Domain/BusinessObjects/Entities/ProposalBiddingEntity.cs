namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalBiddingEntity {

        public string CodigoProduto { get; set; }
        public string CodigoModalidade { get; set; }
        public int DiasPrazoVigencia { get; set; }
        public decimal ValorGarantia { get; set; }
        public string NumeroLicitacao { get; set; }
        public string CpfCnpjTomador { get; set; }
        public InsuredEntity Segurado { get; set; }
    }
}
