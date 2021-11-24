using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace InsuranceApi.Domain.BusinessObjects.MessagesService {
    [DataContract]
    public abstract class BaseRequest {

        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        public virtual void AddHeader(string key, string value) {
            if (string.IsNullOrEmpty(value))
                return;

            if (!_headers.Contains(new KeyValuePair<string, string>(key, value))) {
                if (!_headers.ContainsKey(key)) {
                    _headers.Add(key, value);
                } else {
                    _headers[key] = value;
                }
            }
        }

        public virtual string GetHeader(string key) {
            string outValue;

            if (_headers.TryGetValue(key, out outValue)) {
                return outValue;
            }

            return string.Empty;
        }

        public virtual Dictionary<string, string> DefaultRequestHeaders {
            get {
                return _headers;
            }
        }

        public object BodyObject { get; set; }
    }
}
