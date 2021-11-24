
using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum InsuredInputTypeEnum {
        [Description("Reclamante")]
        Reclamante = 1,
        [Description("Insured")]
        Segurado = 2,
    }

    public static class InsuredInputTypeEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(InsuredInputTypeEnum)).Cast<InsuredInputTypeEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}
