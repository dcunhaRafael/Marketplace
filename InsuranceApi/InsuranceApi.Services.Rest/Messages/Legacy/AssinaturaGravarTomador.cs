using InsuranceApi.Domain.Common.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class AssinaturaGravarTomadorRequest {
        public string cnpj { get; set; }
        public string razao_social { get; set; }
        public string cpf { get; set; }
        public string nome_cliente { get; set; }
        public string email { get; set; }
        public string data_nascimento { get; set; }
        public int numero_documento { get; set; }
        public string ip { get; set; }
        public string user_agent { get; set; }
        public Documento impresso { get; set; }
    }

    public class AssinaturaGravarTomadorResponse {
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTime timestamp { get; set; }
        public int status { get; set; }
        public List<string> erros { get; set; }
    }
}
