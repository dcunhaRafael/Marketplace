using System.ComponentModel;

namespace Domain.Enumerators {
    public enum ActionTypeEnum {
        [Description("Sistema: Main")]
        [DefaultValue("0")]
        Main = 0,

        [Description("Configurações")]
        [DefaultValue("1")]
        Settings = 1,
    }
}
