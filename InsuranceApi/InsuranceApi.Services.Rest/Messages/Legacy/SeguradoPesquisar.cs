using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class SeguradoPesquisarRequest {
        public int? id_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string nm_pessoa { get; set; }
        public bool? dv_ativo { get; set; }
        public bool? dv_endereco_padrao { get; set; }
    }

    public class SeguradoPesquisarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<SeguradoPesquisarItem> Segurado_Pesquisar { get; set; }
    }

    public class SeguradoPesquisarItem {

        public string id_pessoa { get; set; }
        public string nm_pessoa { get; set; }
        public string cd_tp_pessoa { get; set; }
        public string nm_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        //public string nm_pessoa_corretor { get; set; }

        public string id_endereco { get; set; }
        public string id_tp_endereco { get; set; }
        public string nm_tp_endereco { get; set; }
        public string nm_logradouro { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string nm_cep { get; set; }
        public string nm_uf { get; set; }

        public SeguradoPesquisarContatoResponse Contato { get; set; }

    }

    public class SeguradoPesquisarContatoResponse {
        public List<SeguradoPesquisarContatoItem> Contato { get; set; }
    }

    public class SeguradoPesquisarContatoItem {
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string meio_comunicacao { get; set; }
        public string valor_meio_comunicacao { get; set; }
    }
}
