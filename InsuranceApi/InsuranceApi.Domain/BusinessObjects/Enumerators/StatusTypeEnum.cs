using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusTypeEnum {
        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 2,
        [Description("Bloqueado")]
        Bloqueado = 3,
        [Description("Pré-Cadastro")]
        Pre_Cadastro = 4
    }
}
