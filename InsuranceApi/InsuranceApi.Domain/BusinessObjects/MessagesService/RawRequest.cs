using System.Runtime.Serialization;

namespace InsuranceApi.Domain.BusinessObjects.MessagesService {

    [DataContract]
    public class RawRequest : BaseRequest {
        public string RequestUri { get; set; }
    }
}
