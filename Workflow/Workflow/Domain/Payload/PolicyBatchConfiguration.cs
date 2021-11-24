using Domain.Enumerators;
using System.Collections.Generic;

namespace Domain.Payload {
    public class PolicyBatchConfiguration {
        public PolicyBatchConfiguration() {
            this.Mails = new List<PolicyBatchConfigurationMail>();
        }

        public int? PolicyBatchConfigurationId { get; set; }
        public PolicyBatchRenovationEnum? BatchType { get; set; }
        public bool GroupByBroker { get; set; }
        public bool GroupByTaker { get; set; }
        public bool GroupByInsured { get; set; }
        public int? ProcessDays { get; set; }
        public int? CompulsoryIssueDays { get; set; }
        public RecordStatusEnum Status { get; set; }
        public int? LoggedUserId { get; set; }

        public IList<PolicyBatchConfigurationMail> Mails { get; set; }
    }
}
