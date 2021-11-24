using System.ComponentModel;

namespace Domain.Enumerators {
    public enum PolicyBatchRenovationEnum {
        [Description("Renovação")]
        [DefaultValue("0")]
        Renovation = 0,

        [Description("Endosso de aumento de IS")]
        [DefaultValue("1")]
        EndorsementISIncrease = 1,
    }
}
