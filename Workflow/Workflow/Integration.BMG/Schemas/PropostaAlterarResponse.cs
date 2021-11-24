
namespace Integration.BMG.Schemas {

    public class PropostaAlterarResponse : CodigoRetornoResponse {
        public string nr_proposta { get; set; }
        public string id_apolice { get; set; }
        public PropostaPesquisarItem Proposta { get; set; }
    }
}