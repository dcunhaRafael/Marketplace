using System.Collections.Generic;

namespace Domain.Entities {
    public class PolicyBatchConfigurationMail : BaseEntity {
        public PolicyBatchConfigurationMail() {
            this.Destinations = new List<PolicyBatchConfigurationMailDestination>();
        }

        public int? PolicyBatchConfigurationMailId { get; set; }
        public int? PolicyBatchConfigurationId { get; set; }
        public int? DaysBeforeExpiration { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool SendToBroker { get; set; }
        public bool SendToSubscription { get; set; }
        public IList<PolicyBatchConfigurationMailDestination> Destinations { get; set; }
    }
}
