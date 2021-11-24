using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using InsuranceApi.Domain.Common.Converters;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class AssinaturaGravarRequest {
        public string cpf_cnpj { get; set; }
        public string nome_cliente { get; set; }
        public int numero_documento { get; set; }
        public string ip { get; set; }
        public string user_agent { get; set; }
        public Documento impresso { get; set; }
    }

    public class AssinaturaGravarResponse {
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTime timestamp { get; set; }
        public int status { get; set; }
        public List<string> erros { get; set; }
    }

    public class Documento {
        public string nome { get; set; }
        public string bytes { get; set; }
    }
    public class Impresso {
        public string href { get; set; }
    }

    public class AssinaturaConsultarResponse {
        public int id_transacao { get; set; }
        public string cpf_cnpj { get; set; }
        public string nome_cliente { get; set; }
        public int numero_documento { get; set; }
        public string ip { get; set; }
        public string user_agent { get; set; }
        public Impresso impresso_original { get; set; }
        public Impresso impresso_assinado { get; set; }
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTime timestamp_inclusao { get; set; }
    }

    public class AssinaturaImpressaoResponse {
        public int id { get; set; }
        public string nome { get; set; }
        public string bytes { get; set; }
    }
}
