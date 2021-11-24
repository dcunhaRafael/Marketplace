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
        public string nm_situacao_extrado { get; set; }
        public decimal vl_comissao_nao_tributavel { get; set; }
        public int ColunaSemNome13 { get; set; }
        public CorretorDetalheExtratoComissaoImpostos Imposto { get; set; }
        public CorretorDetalheExtratoComissaoItens Itens { get; set; }
    }
}

