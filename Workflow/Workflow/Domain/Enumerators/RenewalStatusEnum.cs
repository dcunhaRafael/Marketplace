using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RenewalStatusEnum {

        [Description("Proposta")]
        [DefaultValue("1")]
        Proposal = 1,

        [Description("Em análise subscrição")]
        [DefaultValue("2")]
        UnderSubscriptionReview = 2,

    }
}
