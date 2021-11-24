using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusCargaTomadorEnum {
        [Description("Pendente")]
        Pendente = 0,
        [Description("Em processo de assinatura")]
        EmProcessoAssinatura = 1,
        [Description("Rejeitado")]
        Rejeitado = 2,
        [Description("Aprovado")]
        Aprovado = 3
    }
}
