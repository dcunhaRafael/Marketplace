namespace Integration.BMG.Schemas {
    public class CorretorDetalheExtratoComissaoItensItem  {
        public long cd_apolice { get; set; }
        public int nr_endosso { get; set; }
        public int nr_parcela { get; set; }
        public int cd_filial { get; set; }
        public string nm_filial { get; set; }
        public int id_pessoa_tomador { get; set; }
        public string nm_tomador { get; set; }
        public int id_pessoa_segurado { get; set; }
        public string nm_segurado { get; set; }
        public decimal valorComissaoTributavel { get; set; }
    }
}

