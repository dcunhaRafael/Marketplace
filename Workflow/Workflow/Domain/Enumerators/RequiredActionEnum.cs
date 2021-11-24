using System.ComponentModel;

namespace Domain.Enumerators {
    public enum RequiredActionEnum {
        [Description("Técnica")]
        [DefaultValue("1")]
        Technical = 1,

        [Description("Operacional")]
        [DefaultValue("2")]
        Operational = 2,

        [Description("Comercial")]
        [DefaultValue("3")]
        Commercial = 3,

        [Description("Tecnologia")]
        [DefaultValue("4")]
        Technology = 4,
    }
}
