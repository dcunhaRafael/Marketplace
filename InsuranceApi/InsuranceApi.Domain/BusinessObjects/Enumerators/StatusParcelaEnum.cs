using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusParcelaEnum {
        [Description("Pendente")]
        P,
        [Description("Quitada")]
        Q,
        [Description("Cancelada")]
        C
    }
}
