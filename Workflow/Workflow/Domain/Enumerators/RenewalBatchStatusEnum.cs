using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RenewalBatchStatusEnum {
        [Description("Pendente")]
        [DefaultValue("1")]
        Pending = 1,

        [Description("Baixado")]
        [DefaultValue("2")]
        Discharged = 2,

        [Description("Cancelado")]
        [DefaultValue("3")]
        Canceled = 3,
    }

}
