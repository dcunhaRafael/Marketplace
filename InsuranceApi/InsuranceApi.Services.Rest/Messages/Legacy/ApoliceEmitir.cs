namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class ApoliceEmitirRequest {
        public string id_apolice { get; set; }
        public string id_usuario { get; set; }
        public string id_endosso { get; set; }
        public string dv_assinatura_proposta { get; set; }
        public string dt_assinatura_proposta { get; set; }
        public string nm_observacao_assinatura { get; set; }
    }

    public class ApoliceEmitirResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public int id_apolice { get; set; }
        public ApoliceEmitirApolice Apolice { get; set; }
    }
    public class ApoliceEmitirApolice {
        public int cd_status { get; set; }
        public string cd_apolice { get; set; }
    }
}
