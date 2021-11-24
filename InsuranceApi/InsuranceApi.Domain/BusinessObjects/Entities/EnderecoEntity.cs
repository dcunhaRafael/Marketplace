namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class EnderecoEntity {

        public EnderecoEntity() {
            TipoEndereco = new TipoEnderecoEntity();
        }

        public int? IdEndereco { get; set; }
        public TipoEnderecoEntity TipoEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public int? IdUf { get; set; }
        public int  IdCidade { get; set; }
        public string UF { get; set; }
        public int CodigoRetorno { get; set; }
        public string MesagemRetorno { get; set; }
    }
}
