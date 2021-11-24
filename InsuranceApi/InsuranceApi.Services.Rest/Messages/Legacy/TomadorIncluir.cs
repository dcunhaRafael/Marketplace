using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Services.Rest.Messages.Legacy {

    public class TomadorIncluirRequest {
        public string nm_pessoa { get; set; }
        public int cd_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string id_usuario { get; set; }
        public string obs_ccg_subscricao { get; set; }
        public string cd_classe_risco { get; set; }
        public string vl_lim_credito { get; set; }
        public string vl_taxa { get; set; }
        public string meio_pagamento { get; set; }
        public string nr_banco { get; set; }
        public string cd_tp_conta_banco { get; set; }
        public string nr_agencia { get; set; }
        public string dac_agencia { get; set; }
        public string nr_conta_corrente { get; set; }
        public string dac_conta_corrente { get; set; }
        public string cd_tp_dominio { get; set; }

        public EnderecoItem TomadorInserir_Endereco { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ContatoItem TomadorInserir_Contato { get; set; }
    }
    public class TomadorIncluirResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public int id_pessoa { get; set; }
        public TomadorIncluirRetornoEndereco TomadorInserir_Endereco { get; set; }
        public TomadorIncluirRetornoContato InsereTomador_Contato { get; set; }
    }
    public class TomadorIncluirRetornoEndereco {
        public string id_endereco { get; set; }
    }
    public class TomadorIncluirRetornoContato {
        public int id_pessoa { get; set; }
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string meio_comunicacao { get; set; }
        public string valor_meio_comunicacao { get; set; }
    }

}
