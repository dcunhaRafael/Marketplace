namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class EnderecoItem {
        public int id_tp_endereco { get; set; }
        public string nm_endereco { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public int cd_uf { get; set; }
        public string nm_cep { get; set; }
        public int id_local { get; set; }
        public bool dv_endereco_padrao { get; set; }
    }

    public class EnderecoAlteraItem {
        public int id_endereco_altera { get; set; }
        public int id_tp_endereco_altera { get; set; }
        public string nm_endereco_altera { get; set; }
        public string nr_rua_endereco_altera { get; set; }
        public string nm_complemento_altera { get; set; }
        public string nm_bairro_altera { get; set; }
        public string nm_cidade_altera { get; set; }
        public int cd_uf_altera { get; set; }
        public string nm_cep_altera { get; set; }
        public bool dv_endereco_padrao_altera { get; set; }
    }
}
