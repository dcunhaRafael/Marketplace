using InsuranceApi.Domain.Common.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum InsuredObjectPrintModeEnum {

        [Description("Sempre imprimir")]
        Always = 1,

        [Description("Se pelo menos uma variável for preenchida")]
        IfAtLeastOneVariableIsFilled = 2,

        [Description("Se todas as variáveis forem preenchidas")]
        IfAllVariablesAreFilled = 3,
    }

    public static class InsuredObjectPrintModeEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(InsuredObjectPrintModeEnum)).Cast<InsuredObjectPrintModeEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}
