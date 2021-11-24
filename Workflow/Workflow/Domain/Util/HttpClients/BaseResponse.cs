using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace Domain.Util.HttpClients {
    [DataContract]
    public abstract class BaseResponse {
        /// <summary>
        /// Retorna (true/false)
        /// </summary>
        [DataMember]
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagem retornada pela execução
        /// </summary>
        [DataMember]
        public string Mensagem { get; set; }

        /// <summary>
        /// Mensagem do erro ocorrido durante a execução do serviço
        /// </summary>
        [DataMember]
        public string Erro { get; set; }

        /// <summary>
        /// Retorno todo objeto recebido, em caso de problemas de conversao conseguimos saber o que foi retornado pelo serviço
        /// </summary>
        [DataMember]
        public string Conteudo { get; set; }

        [DataMember]
        public string RequestHeader { get; set; }

        [DataMember]
        public string ResponseHeader { get; set; }

        [DataMember]
        public HttpStatusCode StatusCode { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public string RequestUri { get; set; }


        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public virtual void AddHeader(string key, string value) {
            if (!_headers.Contains(new KeyValuePair<string, string>(key, value))) {
                if (!_headers.ContainsKey(key)) {
                    _headers.Add(key, value);
                } else {
                    _headers[key] = value;
                }
            }
        }

        public virtual string GetHeader(string key) {

            if (_headers.TryGetValue(key, out string outValue)) {
                return outValue;
            }

            return string.Empty;
        }

        public virtual Dictionary<string, string> DefaultResponseHeaders {
            get {
                return _headers;
            }
        }
    }
}