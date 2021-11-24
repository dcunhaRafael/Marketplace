using System.ComponentModel;

namespace Domain.Enumerators {
    public enum OccurrenceActionTypeEnum {
        [Description("Criação")]
        [DefaultValue("1")]
        Creation = 1,

        [Description("Aprovação")]
        [DefaultValue("2")]
        Approval = 2,

        [Description("Recusa")]
        [DefaultValue("3")]
        Refusal = 3,

        [Description("Encaminhamento")]
        [DefaultValue("4")]
        Forwarding = 4,

        [Description("Documento anexado")]
        [DefaultValue("5")]
        DocumentAttach = 5,

        [Description("Documento removido")]
        [DefaultValue("6")]
        DocumentRemoved = 6
    }
}