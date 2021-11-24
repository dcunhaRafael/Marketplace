using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RenewalBatchTypeEnum {

        [Description("Renovação")]
        [DefaultValue("1")]
        Renewal = 1,

        [Description("Aumento de IS")]
        [DefaultValue("2")]
        InsuredAmountIncrease = 2,

    }

}
