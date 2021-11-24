
namespace Integration.BMG.Schemas {
    public class TomadorInserirContatoResponse {
        public int id_pessoa { get; set; }
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string meio_comunicacao { get; set; }
        public string valor_meio_comunicacao { get; set; }
    }
}