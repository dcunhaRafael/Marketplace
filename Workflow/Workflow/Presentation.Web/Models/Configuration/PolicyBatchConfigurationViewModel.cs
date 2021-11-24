using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Models.Configuration {
    public class PolicyBatchConfigurationViewModel {
        public PolicyBatchConfigurationViewModel() {
            Filters = new PolicyBatchConfigurationFilters();
            Configurations = new List<PolicyBatchConfiguration>();
            CurrentItem = new PolicyBatchConfiguration();
            Mails = new List<PolicyBatchConfigurationMail>();
            CurrentMail = new PolicyBatchConfigurationMail();
            Destinations = new List<User>();
        }

        public PolicyBatchConfigurationFilters Filters { get; set; }
        public IList<PolicyBatchConfiguration> Configurations { get; set; }

        public PolicyBatchConfiguration CurrentItem { get; set; }
        public IList<PolicyBatchConfigurationMail> Mails { get; set; }
        public PolicyBatchConfigurationMail CurrentMail { get; set; }
        public IList<User> Destinations { get; set; }

        public bool IsEditable { get; set; }
      
    }
}
