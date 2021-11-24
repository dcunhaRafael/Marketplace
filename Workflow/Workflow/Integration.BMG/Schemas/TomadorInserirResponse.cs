using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorInserirResponse : CodigoRetornoResponse {
        public string id_pessoa { get; set; }
        public TomadorInserirEnderecoResponse TomadorInserir_Endereco { get; set; }
        public TomadorInserirContatoResponse InsereTomador_Contato { get; set; }
    }
}