
namespace Integration.BMG.Schemas {

    public class TomadorAlterarEnderecoRequest {
        public string id_endereco_altera { get; set; }
        public string id_tp_endereco_altera { get; set; }
        public string nm_endereco_altera { get; set; }
        public string nr_rua_endereco_altera { get; set; }
        public string nm_complemento_altera { get; set; }
        public string nm_bairro_altera { get; set; }
        public string nm_cidade_altera { get; set; }
        public string cd_uf_altera { get; set; }
        public string nm_cep_altera { get; set; }
        public bool dv_endereco_padrao_altera { get; set; }
        public string id_local { get; set; }
    }

}