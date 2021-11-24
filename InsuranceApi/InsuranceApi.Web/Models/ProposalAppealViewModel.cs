namespace InsuranceApi.Web.ViewModels {
    public class ProposalAppealViewModel {

        public string CodigoProduto { get; set; }
        public string CodigoModalidade { get; set; }
        public int CodigoPrazoAno { get; set; }
        public string NumeroProcesso { get; set; }
        public int CodigoTribunal { get; set; }
        public int? CodigoVara { get; set; }
        public int CodigoTipoRecurso { get; set; }
        public decimal? ValorDepositoRecursal { get; set; }
        public decimal? PercentualAgravo { get; set; }
        public string CpfCnpjTomador { get; set; }
        public string CpfCnpjReclamente { get; set; }
        public string NomeRazaoSocialReclamente { get; set; }
        public string CepReclamente { get; set; }
        public string NumeroReclamente { get; set; }
        public string EnderecoReclamente { get; set; }
        public string UfReclamente { get; set; }
        public string CidadeReclamente { get; set; }
        public string BairroReclamente { get; set; }
        public string ComplementoReclamente { get; set; }

    }
}
