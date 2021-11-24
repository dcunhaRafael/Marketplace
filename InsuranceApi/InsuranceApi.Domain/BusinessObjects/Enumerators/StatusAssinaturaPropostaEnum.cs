using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusAssinaturaPropostaEnum {
        [Description("Assinatura Não Solicitada")]
        AssinaturaNaoSolicitada = 0,

        [Description("Pendente de Assinatura")]
        PendenteAssinatura = 1,

        [Description("Pendente de Assinatura da Empresa")]
        PendenteAssinaturaTomador = 2,

        [Description("Pendente de Assinatura do Broker")]
        PendenteAssinaturaCorretor = 3,

        [Description("Cancelado")]
        Cancelado = 4,

        [Description("Assinado")]
        Assinado = 5,
    }

    public static class StatusAssinaturaPropostaEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(StatusAssinaturaPropostaEnum)).Cast<StatusAssinaturaPropostaEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
