using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RenovationStatusEnum {
        [Description("Pré-aprovada")]
        [DefaultValue("0")]
        PreApproved = 0,

        [Description("Proposta em análise")]
        [DefaultValue("1")]
        ProposalUnderReview = 1,

        [Description("Em análise subscrição")]
        [DefaultValue("2")]
        UnderSubscriptionReview = 2,
    }
}
