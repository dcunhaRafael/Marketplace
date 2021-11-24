using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum LawsuitInputTypeEnum {
        [Description("Dados Cível ou Trabalhista")]
        DadosCivilTrabalhista = 1,
        [Description("Dados Fiscal/Tributário")]
        DadosFiscalTributario = 2,
    }

    public static class LawsuitInputTypeEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(LawsuitInputTypeEnum)).Cast<LawsuitInputTypeEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}
