using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum TipoDominioDocumentoEnum {
        [Description("Endosso")]
        Endosso = 2,
        [Description("Taker")]
        Tomador = 5,
    }
}
