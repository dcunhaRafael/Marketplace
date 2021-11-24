using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum UserEntityTypeEnum {
        [Description("Advogado")]
        Advogado = 15,
        [Description("Comissionado")]
        Comissionado = 7,
        [Description("Corretor")]
        Corretor = 2,
        [Description("Empresa")]
        Empreaa = 1,
        [Description("Escritório de Advocacia")]
        EscritorioAdvocacia = 14,
        [Description("Usuário")]
        Usuario = 12,
    }
}
