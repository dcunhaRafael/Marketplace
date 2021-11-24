using System;
using Newtonsoft.Json;
using InsuranceApi.Domain.Common.Extension;

namespace InsuranceApi.Domain.Common.Converters {
    public class TimestampJsonConverter : JsonConverter {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            long ts = serializer.Deserialize<long>(reader);
            return ts.ToDateTime();
        }

        public override bool CanConvert(Type type) {
            return typeof(DateTime).IsAssignableFrom(type);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            //TODO: PENDENTE DE IMPLEMEntAÇÂO
        }

        public override bool CanRead {
            get { return true; }
        }
    }
}
