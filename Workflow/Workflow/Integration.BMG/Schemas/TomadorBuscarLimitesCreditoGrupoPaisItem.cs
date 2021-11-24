namespace Integration.BMG.Schemas {

    public class TomadorBuscarLimitesCreditoGrupoPaisItem {
        public int id_pessoa_pai { get; set; }
        public int id_tomador_pai { get; set; }
        public string nm_pessoa_pai { get; set; }
        public int cd_tp_pessoa_pai { get; set; }
        public string nm_tipo_pessoa_pai { get; set; }
        public long nr_cnpj_cpf_pai { get; set; }
        public decimal pe_participacao { get; set; }
        public bool dv_gera_acumulo { get; set; }
        public decimal vl_lim_credito_pai { get; set; }
        public decimal vl_disponivel_credito_ress_pai { get; set; }
        public string dt_validade_tomador_pai { get; set; }
        public string dt_validade_compliance_pai { get; set; }
    }
}