using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum PaymentDayEnum {
        [Description("Primeiro dia do mês")]
        PrimeiroDiaMes = 1,
        [Description("Último dia do mês")]
        UltimoDiaMes = 2
    }

    public static class PaymentDayEnumUtil {
        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(PaymentDayEnum)).Cast<PaymentDayEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}
