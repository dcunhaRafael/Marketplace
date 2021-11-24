using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class CorretorBuscarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<CorretorBuscarItem> Corretor_Buscar { get; set; }
    }

    public class CorretorBuscarItem {
        public string id_pessoa { get; set; }
        public string nm_pessoa { get; set; }
        public string cd_tipo_pessoa { get; set; }
        public string nm_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string id_endereco { get; set; }
        public string id_tp_endereco { get; set; }
        public string nm_tp_endereco { get; set; }
        public string nm_logradouro { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string nm_cep { get; set; }
        public string cd_uf { get; set; }
        public string nm_uf { get; set; }
    }

}