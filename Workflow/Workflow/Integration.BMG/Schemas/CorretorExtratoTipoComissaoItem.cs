namespace Integration.BMG.Schemas {

    public class CorretorExtratoTipoComissaoItem {
        public string id_pessoa_corretor { get; set; }
        public int codigoTipoComissao { get; set; }
        public string descricaoTipoComissao { get; set; }
        public decimal valorComissao { get; set; }
        public int cd_usuario_autenticacao { get; set; }
    }
}

