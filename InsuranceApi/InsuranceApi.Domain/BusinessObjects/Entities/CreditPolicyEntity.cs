namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class CreditPolicyEntity {

        public CreditPolicyEntity() {
            Endereco = new EnderecoEntity();
        }
        public string RazaoSocial { get; set; }
        public decimal? QualidadeEmpresa { get; set; }
        public int? Pontuacao { get; set; }
        public decimal? LimiteCredito { get; set; }
        public string Rating { get; set; }
        public decimal? Taxa { get; set; }
        public string JsonRecord { get; set; }
        public EnderecoEntity Endereco { get; set; }
    }
}
