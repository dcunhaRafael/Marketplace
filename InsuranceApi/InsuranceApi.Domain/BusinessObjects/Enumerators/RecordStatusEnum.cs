﻿using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum RecordStatusEnum {
        [Description("Ativo")]
        Ativo = 1,

        [Description("Inativo")]
        Inativo = 2,
    }

    public static class RecordStatusEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(RecordStatusEnum)).Cast<RecordStatusEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}
