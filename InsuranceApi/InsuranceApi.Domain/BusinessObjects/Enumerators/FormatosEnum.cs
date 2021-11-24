using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum FormatosEnum {
        [Description("json")]
        json,
        [Description("b64")]
        b64,
        [Description("pdf")]
        pdf
    }
}
