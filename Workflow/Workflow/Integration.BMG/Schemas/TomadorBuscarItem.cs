using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class TomadorBuscarItem {
        public string id_pessoa { get; set; }
        public string nm_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string cd_tp_pessoa { get; set; }
        public string nm_tipo_pessoa { get; set; }
        //public string dv_ativo { get; set; }
        //public string nm_ativo { get; set; }
        //public string cd_status_tomador { get; set; }
        //public string nm_status_tomandor { get; set; }
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
        public string dt_validade_cadastro_ress { get; set; }
        public string nm_tp_dominio { get; set; }
        public string id_endereco { get; set; }
        public string id_tp_endereco { get; set; }
        public string nm_tp_endereco { get; set; }
        public string nm_logradouro { get; set; }
        public string nr_rua_endereco { get; set; }
        public string nm_complemento { get; set; }
        public string nm_bairro { get; set; }
        public string nm_cidade { get; set; }
        public string id_local { get; set; }
        public string nm_cep { get; set; }
        public string cd_uf { get; set; }
        public string nm_uf { get; set; }
        public string vl_is_total { get; set; }
        public string vl_saldo_disponivel { get; set; }
        public bool dv_ativo { get; set; }
        public string nm_status_ccg { get; set; }
        public List<TomadorBuscarItemCorretor> Corretor_Tomador { get; set; }
    }
}