using Newtonsoft.Json;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class SeguradoIncluirRequest {
        public string nm_pessoa { get; set; }
        public int cd_tipo_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        [JsonProperty("SeguradoInserir_Endereco", NullValueHandling = NullValueHandling.Ignore)]
        public EnderecoItem SeguradoInserir_Endereco { get; set; }
        [JsonProperty("Contato", NullValueHandling = NullValueHandling.Ignore)]
        public ContatoItem Contato { get; set; }
    }

    public class SeguradoIncluirResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public string id_pessoa { get; set; }
        public SeguradoIncluirResponseEndereco SeguradoInserir_Endereco { get; set; }
        public SeguradoIncluirResponseContato Contato { get; set; }
    }

    public class SeguradoIncluirResponseEndereco {
        public string id_endereco { get; set; }
    }

    public class SeguradoIncluirResponseContato {
        public string id_pessoa { get; set; }
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string meio_comunicacao { get; set; }
        public string valor_meio_comunicacao { get; set; }
    }
}
