using System.ComponentModel;

namespace Domain.Enumerators {
    public enum OccurrenceStatusEnum {
        [Description("Pendente")]
        [DefaultValue("1")]
        Pending = 1,

        [Description("Aprovada")]
        [DefaultValue("2")]
        Approved = 2,

        [Description("Recusada")]
        [DefaultValue("3")]
        Refused = 3,
    }

}
