namespace Integration.BMG.Schemas {
    public class CorretorExtratoComissaoItem {
        public int cd_extrato { get; set; }
        public string id_pessoa_corretor { get; set; }
        public string cd_susep { get; set; }
        public string nm_corretor { get; set; }
        public string dt_competencia { get; set; }
        public int nr_qtd_lancamentos { get; set; }
        public string dt_abertura { get; set; }
        public string dt_fechamento { get; set; }
        public decimal vl_comissao { get; set; }
        public string dt_pagto { get; set; }
        public string nm_situacao_extrado { get; set; }
        public int cd_usuario_autenticacao { get; set; }
    }
}

