
namespace Integration.BMG.Schemas {

    public class PropostaGravarResponse : CodigoRetornoResponse {
        public string nr_proposta { get; set; }
        public string id_apolice { get; set; }
        public PropostaPesquisarItem Proposta { get; set; }
    }
}