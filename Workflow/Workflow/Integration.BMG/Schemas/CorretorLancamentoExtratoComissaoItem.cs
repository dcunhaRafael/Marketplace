namespace Integration.BMG.Schemas {
    public class CorretorLancamentoExtratoComissaoItem {
        public int cd_extrato { get; set; }
        public int cd_lancamento { get; set; }
        public string ColunaSemNome2 { get; set; }
        public string dt_competencia { get; set; }
        public string ColunaSemNome4 { get; set; }
        public decimal nr_proposta { get; set; }
        public decimal nr_apolice { get; set; }
        public int nr_endosso { get; set; }
        public int nr_parcela { get; set; }
        public int id_pessoa_tomador { get; set; }
        public string nm_tomador { get; set; }
        public int id_pessoa_segurado { get; set; }
        public string nm_segurado { get; set; }
        public int cd_tipo_comissao { get; set; }
        public string nm_tp_comissao { get; set; }
        public decimal Pe_comissao { get; set; }
        public decimal vl_premio_base { get; set; }
        public int cd_ramo { get; set; }
        public string nm_ramo { get; set; }
    }
}

