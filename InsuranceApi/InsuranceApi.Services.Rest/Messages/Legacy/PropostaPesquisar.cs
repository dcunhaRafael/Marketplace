using System;
using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class PropostaPesquisarRequest {
        public long? id_apolice { get; set; }
        public int? cd_proposta { get; set; }
        public string cd_susep { get; set; }
        public int? id_pessoa_tomador { get; set; }
        public int? id_pessoa_cliente { get; set; }
        public int? cd_produto { get; set; }
        public int? cd_status { get; set; }
        public int? id_usuario { get; set; }
        public DateTime? data_inicial { get; set; }
        public DateTime? data_final { get; set; }
    }

    public class PropostaPesquisarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<PropostaPesquisarItem> Proposta_Pesquisar { get; set; }
    }

    public class PropostaPesquisarItem {
        public PropostaPesquisarItem() {
            ParcelaProposta = new ParcelaPropostaItem();
        }

        public string id_apolice { get; set; }
        public string id_endosso { get; set; }
        public string cd_apolice { get; set; }
        public string dt_proposta { get; set; }
        public string dt_validade { get; set; }
        public string dt_inicio_vigencia { get; set; }
        public string dt_fim_vigencia { get; set; }
        public string nm_status { get; set; }
        public string cd_status { get; set; }
        public string cd_proposta { get; set; }
        public string cd_produto { get; set; }
        public string nm_produto { get; set; }

        public string id_pessoa_corretor { get; set; }
        public string nm_pessoa_corretor { get; set; }

        public string id_pessoa_tomador { get; set; }
        public string nm_pessoa_tomador { get; set; }
        public string nr_cnpj_cpf_tomador { get; set; }
        public string cd_tp_pessoa_tomador { get; set; }
        public string nm_tp_pessoa_tomador { get; set; }
        public string id_endereco_tomador { get; set; }
        public string id_tp_endereco_tomador { get; set; }
        public string nm_tp_endereco_tomador { get; set; }
        public string nm_logradouro_tomador { get; set; }
        public string nr_rua_endereco_tomador { get; set; }
        public string nm_complemento_tomador { get; set; }
        public string nm_bairro_tomador { get; set; }
        public string nm_cidade_tomador { get; set; }
        public string cd_uf_tomador { get; set; }
        public string nm_uf_tomador { get; set; }
        public string nm_cep_tomador { get; set; }

        public string id_pessoa_segurado { get; set; }
        public string nm_pessoa_segurado { get; set; }
        public string cd_tp_pessoa_segurado { get; set; }
        public string nm_tp_pessoa_segurado { get; set; }
        public string nr_cnpj_cpf_segurado { get; set; }
        public string id_endereco_segurado { get; set; }
        public string id_tp_endereco_segurado { get; set; }
        public string nm_tp_endereco_segurado { get; set; }
        public string nm_logradouro_segurado { get; set; }
        public string nr_rua_endereco_segurado { get; set; }
        public string nm_complemento_segurado { get; set; }
        public string nm_bairro_segurado { get; set; }
        public string nm_cidade_segurado { get; set; }
        public string cd_uf_segurado { get; set; }
        public string nm_uf_segurado { get; set; }
        public string nm_cep_segurado { get; set; }

        public string dados_cobranca_dt_vencimento_pparcela { get; set; }
        public string dados_cobranca_nr_dia_cobranca { get; set; }
        public string dados_cobranca_cd_forma_pagamento_pparcela { get; set; }
        public string dados_cobranca_nm_forma_pagamento_pparcela { get; set; }
        public string dados_cobranca_cd_forma_pagamento_demais_parc { get; set; }
        public string dados_cobranca_nm_forma_pagamento_demais_parcela { get; set; }
        public string dados_cobranca_id_produto_parc_premio { get; set; }
        public string dados_cobranca_nm_parcelamento { get; set; }
        public string dados_cobranca_id_periodo_pagamento { get; set; }
        public string dados_cobranca_nm_periodo { get; set; }

        public string dados_garantia_id_produto_cobertura { get; set; }
        public string dados_garantia_nm_cobertura { get; set; }
        public string dados_garantia_vl_premio_tarifario { get; set; }
        public string dados_garantia_Pe_comissao { get; set; }
        public string dados_garantia_vl_taxa_moeda { get; set; }
        public string dados_garantia_vl_adicional { get; set; }
        public string dados_garantia_Vl_comissao { get; set; }
        public string dados_garantia_vl_is { get; set; }
        public string dados_garantia_vl_taxa_risco { get; set; }
        public string dados_garantia_cd_moeda { get; set; }
        public string dados_garantia_vl_iof { get; set; }
        public string dados_garantia_pe_iof { get; set; }

        public string objeto_segurado_nm_objeto_segurado { get; set; }

        public string cd_usuario_inclusao { get; set; }
        public string nm_usuario_inclusao_login { get; set; }

        public string cd_usuario_altracao { get; set; }
        public string nm_usuario_alteracao_login { get; set; }

        public ParcelaPropostaItem ParcelaProposta { get; set; }
    }

    public class ParcelaPropostaItem {
        public ParcelaPropostaItem() {
            ParcelaProposta = new List<ParcelaItem>();
        }
        public List<ParcelaItem> ParcelaProposta { get; set; }
    }

    public class ParcelaItem {
        public string nr_parcela { get; set; }
        public string vl_adicional { get; set; }
        public string dt_vencimento { get; set; }
        public string vl_iof { get; set; }
        public string vl_premio_tarifario { get; set; }
        public string vl_premio_total { get; set; }
        public string vl_custo { get; set; }
        public string dv_situacao { get; set; }
    }
}
