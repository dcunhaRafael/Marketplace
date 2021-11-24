using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum TipoSeguradoEnum {
        [Description("CivilCourt")]
        Vara = 0,
        [Description("LaborCourt")]
        Tribunal = 1,
        [Description("Reclamante")]
        Reclamante = 2,
    }

    public static class TipoSeguradoEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(TipoSeguradoEnum)).Cast<TipoSeguradoEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}