using System.ComponentModel;

namespace Domain.Enumerators {
    public enum FixedDomainGroupNameEnum {
        [Description("Faixas de agrupamento dos boletos com pagamentos em atraso")]
        [DefaultValue("LatePaymentSlipAgings")]
        LatePaymentSlipAgings,
    }
}
