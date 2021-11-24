namespace InsuranceApi.Web.ViewModels {
    public class ProposalBiddingViewModel {

        public string CodigoProduto { get; set; }
        public string CodigoModalidade { get; set; }
        public int DiasPrazoVigencia { get; set; }        
        public decimal ValorGarantia { get; set; }
        public string NumeroLicitacao { get; set; }
        public string CpfCnpjTomador { get; set; }
        public InsuredViewModel Segurado { get; set; }
    }
}
