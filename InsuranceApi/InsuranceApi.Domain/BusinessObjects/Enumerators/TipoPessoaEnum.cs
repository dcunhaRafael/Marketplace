using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Common.Validations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum TipoPessoaEnum {
        [Description("Pessoa Física")]
        PessoaFisica = 1,

        [Description("Pessoa Jurídica")]
        PessoaJuridica = 2,

        [Description("Orgão Público")]
        OrgãoPublico = 3,

        [Description("Consórcio de Empresas")]
        ConsorcioEmpresas = 4,

        [Description("Pessoa Júridica sem Domicílio no País")]
        PJSemDomicilioPais = 5,

        [Description("Pessoa Física sem Domicílio no País")]
        PFSemDomicilioPais = 6
    }

    public static class TipoPessoaEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(TipoPessoaEnum)).Cast<TipoPessoaEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(), status.GetEnumDescription());
        }

        /// <summary>
        /// Valida se o documento é um CPF ou CNPJ
        /// </summary>
        /// <param name="documentNumber"></param>
        /// <returns></returns>
        public static TipoPessoaEnum? CheckDocumentNumber(string documentNumber) {
            if (documentNumber != null) {
                if (documentNumber.Length == 11) {
                    if (ValidationHelper.IsCpf(documentNumber.ToString())) {
                        return TipoPessoaEnum.PessoaFisica;
                    }
                } else if (documentNumber.Length == 14) {
                    if (ValidationHelper.IsCnpj(documentNumber.ToString())) {
                        return TipoPessoaEnum.PessoaJuridica;
                    }
                }
            }

            return null;
        }
    }
}
