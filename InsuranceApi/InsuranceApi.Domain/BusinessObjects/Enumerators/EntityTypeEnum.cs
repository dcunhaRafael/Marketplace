using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum EntityTypeEnum {
        [Description("Taker")]
        Tomador = 1,
        [Description("Produtor")]
        Produtor = 2,
        [Description("Fornecedor")]
        Fornecedor = 3,
        [Description("Vendedor")]
        Vendedor = 4,
        [Description("Cliente")]
        Cliente = 5,
        [Description("Comissionado")]
        Comissionado = 7,
        [Description("Usuário")]
        Usuario = 12,

        [Description("Escritório de Advocacia")]
        EscritorioAdvocacia = 14,
        [Description("Advogado")]
        Advogado = 15,
    }

    public static class EntityTypeEnumUtil {
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from tipoEnti in System.Enum.GetValues(typeof(EntityTypeEnum)).Cast<EntityTypeEnum>()
                   select new KeyValuePair<string, string>(((int)tipoEnti).ToString(),
                                                            tipoEnti.GetEnumDescription());
        }
    }
}
