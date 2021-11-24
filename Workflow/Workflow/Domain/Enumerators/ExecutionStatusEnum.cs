using System.ComponentModel;

namespace Domain.Enumerators {
    public enum ExecutionStatusEnum {
        [Description("Em execução")]
        Running = 0,

        [Description("Finalizado com sucesso")]
        FinishedSuccess = 1,

        [Description("Finalizado com erro")]
        FinishedError = 2,
    }
}
