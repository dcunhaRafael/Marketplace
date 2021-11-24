using Domain.Enumerators;
using System.Collections.Generic;

namespace Domain.Payload {
    public class PolicyBatchConfigurationMail  {
        public PolicyBatchConfigurationMail() {
            this.Destinations = new List<PolicyBatchConfigurationMailDestination>();
        }

        public int? PolicyBatchConfigurationMailId { get; set; }
        public int? PolicyBatchConfigurationId { get; set; }
        public int? DaysBeforeExpiration { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool SendToBroker { get; set; }
        public bool SendToTaker { get; set; }
        public RecordStatusEnum Status { get; set; }
        public int? LoggedUserId { get; set; }
        public IList<PolicyBatchConfigurationMailDestination> Destinations { get; set; }
    }
}
