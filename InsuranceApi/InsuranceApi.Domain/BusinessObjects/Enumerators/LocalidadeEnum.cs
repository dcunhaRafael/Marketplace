using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators
{
    public enum  LocalidadeEnum
    {
        [Description("Município")]
        [DefaultValue("M")]
        Município = 1,

        [Description("Distrito")]
        [DefaultValue("D")]
        Distrito = 2,

        [Description("Povoado")]
        [DefaultValue("P")]
        Povoado = 3,

        [Description("Região Administrativa")]
        [DefaultValue("R")]
        RegiaoAdministrativa = 4,
    }

    public static class LocalidadeEnumUtil
    {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList()
        {
            return from status in System.Enum.GetValues(typeof(LocalidadeEnum)).Cast<LocalidadeEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
