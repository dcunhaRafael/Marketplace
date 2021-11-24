using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorAlterarResponse : CodigoRetornoResponse {
        public TomadorInserirContatoResponse TomadorInserir_Endereco { get; set; }
        public TomadorInserirContatoResponse TomadorAlterar_Endereco { get; set; }
        public TomadorInserirContatoResponse InsereTomador_Contato { get; set; }
    }
}