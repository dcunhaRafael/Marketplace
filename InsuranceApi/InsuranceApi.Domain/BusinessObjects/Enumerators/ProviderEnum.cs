using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum ProviderEnum {

        [Description("Ebix Matriz")]
        EbixSP,
        [Description("Ebix RJ")]
        EbixRJ,
        [Description("i4pro")]
        i4pro,
        [Description("Transunion")]
        Transunion
    }
}