
namespace Integration.BMG.Schemas {
    public class TomadorBuscarItemCorretor {
        public string id_pessoa { get; set; }
        public string nm_pessoa { get; set; }
        public string cd_susep_corretor { get; set; }
        public string id_usuario_corretor { get; set; }
        public string cd_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string id_endereco { get; set; }
        public string nm_logradouro { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string id_local { get; set; }
        public string nm_uf { get; set; }
        public string cd_uf { get; set; }
        public string nm_cep { get; set; }
    }
}