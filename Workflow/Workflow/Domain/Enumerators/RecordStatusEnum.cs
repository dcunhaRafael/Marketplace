using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RecordStatusEnum {
        [Description("Inativo")]
        [DefaultValue("0")]
        Inactive = 0,

        [Description("Ativo")]
        [DefaultValue("1")]
        Active = 1,
    }
}
