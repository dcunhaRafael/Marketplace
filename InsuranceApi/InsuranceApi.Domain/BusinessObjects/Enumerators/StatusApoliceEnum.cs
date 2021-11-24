using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusApoliceEnum {
        [Description("Desconhecido")]
        Desconhecido = -1,

        [Description("Emitida")]
        Emitida = 7,

        [Description("Cancel. Atenc. Vigência")]
        CanceladoAtencVigência = 8,

        [Description("Cancelada")]
        Cancelada = 9,

        [Description("Cancel. Inad. Sistema")]
        CanceladoInadSistema = 10,

        [Description("Cancel. por Sinistro")]
        CanceladoPorSinistro = 11,

        [Description("Cancelado Inadimpl.")]
        CanceladoPorInadimplencia = 12,

        [Description("Em Processo de Análise")]
        EmProcessoDeAnálise = 13,

        [Description("Suspensa")]
        Suspensa = 14,

        [Description("Substituida")]
        Substituida = 15,

        [Description("Cancel. Resgate")]
        CanceladoPorResgate = 16
    }

    public static class StatusApoliceEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(StatusApoliceEnum)).Cast<StatusApoliceEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
