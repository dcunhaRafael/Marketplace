using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PropostaAssinadaConsultarEntity {

        [JsonProperty("id_transacao")]
        public int IdTransacao { get; set; }

        [JsonProperty(" status-codigo")]
        public string CodigoStatus { get; set; }

        [JsonProperty("status-descricao")]
        public string DescricaoStatus { get; set; }

        [JsonProperty("uuid-comunicacao-externa")]
        public string Guid { get; set; }

        [JsonProperty("lista_documentos")]
        public List<DocumentoRetorno> Documento { get; set; }

        [JsonProperty("lista_assinante")]
        public List<Assinante> assinante { get; set; }

    }

    public class DocumentoRetorno {
        [JsonProperty("numero_documento")]
        public int NumeroDocumento { get; set; }

        [JsonProperty("impresso_original")]
        public string ImpressoOriginal { get; set; }

        [JsonProperty("impresso_assinado")]
        public string ImpressoAssinado { get; set; }

        [JsonConverter(typeof(ParseTimeStampFromStringConverter))]
        [JsonProperty("timestamp_inclusao")]
        public DateTime TimestampInclusao { get; set; }
    }

    public class Assinante {

        [JsonProperty("cpf_cnpj")]
        public string CpfCnpj { get; set; }

        [JsonProperty("nome_cliente")]
        public string Nome { get; set; }

        [JsonProperty("ip")]
        public string IpAddress { get; set; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        [JsonConverter(typeof(ParseTimeStampFromStringConverter))]
        [JsonProperty("timestamp_assinatura")]
        public DateTime TimestampAssinatura { get; set; }

    }

    public class ParseTimeStampFromStringConverter : JsonConverter {
        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var ts = serializer.Deserialize<long>(reader);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ts);
            return epoch.ToLocalTime();
        }

        public override bool CanConvert(Type objectType) {
            return typeof(DateTime).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            //TODO: PENDENTE DE IMPLEMEntAÇÂO
            throw new NotImplementedException();
        }
    }
}
    