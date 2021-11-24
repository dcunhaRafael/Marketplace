
namespace Integration.BMG.Schemas {

    public class PropostaAlterarRequest {
        public string dt_inicio_vigencia { get; set; }
        public string dt_final_vigencia { get; set; }
        public string cd_susep { get; set; }
        public string id_endereco_tomador { get; set; }
        public string id_endereco_cliente { get; set; }
        public string id_usuario { get; set; }
        public string cd_status { get; set; }
        public string dados_cobranca_dt_vencimento_pparcela { get; set; }
        public string dados_cobranca_dias_demais_pparcela { get; set; }
        public string dados_cobranca_cd_forma_pagamento_pparcela { get; set; }
        public string dados_cobranca_cd_forma_pagamento { get; set; }
        public string dados_cobranca_id_produto_parc_premio { get; set; }
        public string dados_cobranca_id_periodo_pagamento { get; set; }
        public string dados_garantia_id_produto_cobertura { get; set; }
        public string dados_garantia_vl_premio_tarifario { get; set; }
        public string dados_garantia_pe_comissao { get; set; }
        public string dados_garantia_vl_adicional { get; set; }
        public string dados_garantia_vl_is { get; set; }
        public string dados_garantia_cd_moeda { get; set; }
        public string dados_garantia_vl_taxa_risco { get; set; }
        public string dados_garantia_vl_premio_total { get; set; }
        public string dados_garantia_vl_iof { get; set; }
        public string objeto_segurado_nm_objeto_segurado { get; set; }
        public string texto_trabalhista_anexo { get; set; }
        public string id_apolice { get; set; }
        public string cd_usuario_inclusao { get; set; }
        public string dv_calculo_manual { get; set; }
    }

}