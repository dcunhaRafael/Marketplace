using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace InsuranceApi.Domain.BusinessObjects.Enumerators
{
    public  enum TipoComissaoEnum
    {
        [Description("Agenciamento")]
        Agenciamento = 1,
        [Description("Corretagem")]
        Corretagem = 2,
        [Description("ProLabore")]
        ProLabore = 3

    }

    public static class TipoComissaoEnumUtil
    {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList()
        {
            return from tipo in System.Enum.GetValues(typeof(TipoComissaoEnum)).Cast<TipoComissaoEnum>()
                   select new KeyValuePair<string, string>(((int)tipo).ToString(),
                                                            tipo.GetEnumDescription());
        }
    }
}
