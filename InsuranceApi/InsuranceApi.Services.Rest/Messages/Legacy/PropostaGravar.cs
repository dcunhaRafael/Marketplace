using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {


    public class PropostaGravarRequest {

        public string dt_inclusao { get; set; }
        public string dt_inicio_vigencia { get; set; }
        public string dt_final_vigencia { get; set; }
        public string cd_susep { get; set; }
        public string id_pessoa_tomador { get; set; }
        public string id_endereco_tomador { get; set; }
        public string id_pessoa_cliente { get; set; }
        public string id_endereco_cliente { get; set; }
        public string id_usuario { get; set; }
        public string cd_produto { get; set; }
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
        public string dados_garantia_vl_taxa_risco { get; set; }

        public string dados_garantia_vl_premio_total { get; set; }
        public string dados_garantia_vl_iof { get; set; }

        public string objeto_segurado_nm_objeto_segurado { get; set; }

        public string texto_trabalhista_anexo { get; set; }

        public string dv_calculo_manual { get; set; }
    }
    public class CorretoresPropostaItem {
        public CorretoresPropostaItem() {
            Corretores = new List<CorretoresItem>();
        }
        public List<CorretoresItem> Corretores { get; set; }
    }

    public class CorretoresItem {
        public string id_pessoa_corretor { get; set; }
        public string cd_tp_comissao { get; set; }
        public string pe_comissao { get; set; }
        public string dv_corretor_lider { get; set; }
    }

    public class PropostaGravarResponse {
        public PropostaGravarResponse() {
            Proposta = new PropostaPesquisarItem();
        }
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public int nr_proposta { get; set; }
        public int id_apolice { get; set; }
        public PropostaPesquisarItem Proposta { get; set; }
    }
}
