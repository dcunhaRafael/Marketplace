using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusRegistroTomadorEnum {
        [Description("Pendente de validação")]
        PendenteValidacao = 0,
        [Description("OK")]
        OK = 1,
        [Description("Possui erros de validação")]
        ComErros = 2
    }
}
