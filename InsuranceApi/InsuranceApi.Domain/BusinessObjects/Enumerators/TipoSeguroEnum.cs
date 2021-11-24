using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum TipoSeguroEnum {
        [Description("Participar de uma licitação")]
        Licitacao = 1,
        [Description("Garantir um contrato ")]
        Contrato = 2,
        [Description("Recursal")]
        Recursal = 3,
        [Description("Judicial Cível")]
        JudicialCivel = 4,
        [Description("Judicial Trabalhista")]
        JudicialTrabalhista = 5,
        [Description("Judicial Fiscal")]
        JudicialFiscal = 6,
    }

    public static class TipoSeguroEnumEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(TipoSeguroEnum)).Cast<TipoSeguroEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
