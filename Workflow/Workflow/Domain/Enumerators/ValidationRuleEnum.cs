using System.ComponentModel;

namespace Domain.Enumerators {
    public enum ValidationRuleEnum {
        [Description("Sempre gerar ocorrência")]
        [DefaultValue("1")]
        AlwaysGenerate = 1,

        [Description("Verificar se segurado bloqueado")]
        [DefaultValue("2")]
        IsInsuredBlocked = 2,

        [Description("Verificar se possui sublimite de crédito para modalidade")]
        [DefaultValue("3")]
        HasCoverageCreditSubLimit = 3,

        [Description("Verificar se existem pendências financeiras")]
        [DefaultValue("4")]
        HasFinancialPending = 4,

        [Description("Validar se possui limite de crédito")]
        [DefaultValue("5")]
        HasCreditLimit = 5,

        [Description("Verificar se CCG foi assinado")]
        [DefaultValue("6")]
        IsCCGSigned = 6,
    }
}
