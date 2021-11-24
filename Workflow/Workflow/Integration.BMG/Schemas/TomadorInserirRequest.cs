using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorInserirRequest {
        public string nm_pessoa { get; set; }
        public string cd_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
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
        public string id_usuario { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TomadorInserirEnderecoRequest TomadorInserir_Endereco { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TomadorInserirContatoRequest TomadorInserir_Contato { get; set; }
    }
}