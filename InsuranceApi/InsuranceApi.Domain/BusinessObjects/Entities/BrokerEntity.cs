namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class BrokerEntity {

        public BrokerEntity() {
            Endereco = new EnderecoEntity();
        }
        public int IdCorretor { get; set; }
        public int IdPessoa { get; set; }
        public int IdExterno { get; set; }
        public string NomeCorretor { get; set; }
        public string SusepCorretor { get; set; }
        public int TipoPessoa { get; set; }
        public string DescricaoTipoPessoa { get; set; }
        public long CpfCnpj { get; set; }
        public EnderecoEntity Endereco { get; set; }
    }
}
