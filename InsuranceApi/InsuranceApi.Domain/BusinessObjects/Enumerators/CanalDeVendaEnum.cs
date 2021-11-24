using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum CanalDeVendaEnum {
        [Description("Planejamento")]
        Planejamento = 0,

        [Description("Ativo")]
        Ativo = 1,

        [Description("Inativo")]
        Inativo = 2,
    }

    public static class SalesChannelStatusEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(CanalDeVendaEnum)).Cast<CanalDeVendaEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
