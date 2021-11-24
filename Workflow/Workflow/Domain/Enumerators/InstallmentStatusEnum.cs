using Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Enumerators {
    public enum InstallmentStatusEnum {
        [Description("Pendente")]
        P,
        [Description("Quitada")]
        Q,
        [Description("Cancelada")]
        C
    }
}
