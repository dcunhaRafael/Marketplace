using System.ComponentModel;

namespace Domain.Enumerators {
    public enum PolicyBatchStatusEnum {
        [Description("Pendente")]
        [DefaultValue("0")]
        Pending = 0,

        [Description("Baixado")]
        [DefaultValue("1")]
        Discharged = 1,
    }
}
