
namespace Integration.BMG.Schemas {

    public class TomadorInserirEnderecoRequest {
        public string id_tp_endereco { get; set; }
        public string nm_endereco { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string id_local { get; set; }
        public string cd_uf { get; set; }
        public string nm_cep { get; set; }
        public bool dv_endereco_padrao { get; set; }
    }

}