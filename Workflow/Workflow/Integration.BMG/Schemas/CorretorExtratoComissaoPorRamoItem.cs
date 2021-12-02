namespace Integration.BMG.Schemas {
    public class CorretorExtratoComissaoPorRamoItem {
        public string id_pessoa_corretor { get; set; }
        public int CodigoRamo { get; set; }
        public string DescricaoRamo { get; set; }
        public int CodigoTipoComissao { get; set; }
        public string DescricaoTipoComissao { get; set; }
        public decimal ValorComissao { get; set; }
        public int cd_usuario_autenticacao { get; set; }
    }
}

