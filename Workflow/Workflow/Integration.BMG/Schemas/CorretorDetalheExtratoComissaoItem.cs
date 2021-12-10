using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class CorretorDetalheExtratoComissaoItem {
        public int cd_extrato { get; set; }
        public string id_pessoa_corretor { get; set; }
        public string nm_corretor { get; set; }
        public string dt_competencia { get; set; }
        public int nr_qtd_lancamentos { get; set; }
        public decimal vl_comissao { get; set; }
        public string dt_pagto { get; set; }
        public string dt_solicitacao_pagamento { get; set; }
        public string nm_situacao_extrato { get; set; }
        public decimal vl_imposto { get; set; }
        public decimal vl_comissao_tributavel { get; set; }
        public decimal vl_comissao_nao_tributavel { get; set; }
        public int nr_recibo { get; set; }
        public int cd_usuario_autenticacao { get; set; }
        public List<CorretorDetalheExtratoComissaoImpostoItem> imposto { get; set; }
        public List<CorretorDetalheExtratoComissaoItensItem> itens { get; set; }
    }
}

