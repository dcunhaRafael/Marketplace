using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum UfEnum {
        [Description("Acre")]
        [DefaultValue("AC")]
        Acre = 27,

        [DefaultValue("AL")]
        [Description("Alagoas")]
        Alagoas = 8,

        [DefaultValue("AP")]
        [Description("Amapá")]
        Amapa = 25,

        [DefaultValue("AM")]
        [Description("Amazonas")]
        Amazonas = 10,

        [DefaultValue("BA")]
        [Description("Bahia")]
        Bahia = 13,

        [DefaultValue("CE")]
        [Description("Ceará")]
        Ceara = 16,

        [DefaultValue("DF")]
        [Description("Distrito Federal")]
        DistritoFederal = 15,

        [DefaultValue("ES")]
        [Description("Espírito Santo")]
        EspiritoSanto = 21,

        [DefaultValue("GO")]
        [Description("Goiás")]
        Goias = 22,

        [DefaultValue("MA")]
        [Description("Maranhão")]
        Maranhao = 4,

        [DefaultValue("MT")]
        [Description("Mato Grosso")]
        MatoGrosso = 24,

        [DefaultValue("MS")]
        [Description("Mato Grosso do Sul")]
        MatoGrossoDoSul = 3,

        [DefaultValue("MG")]
        [Description("Minas Gerais")]
        MinasGerais = 17,

        [DefaultValue("PA")]
        [Description("Pará")]
        Para = 20,

        [DefaultValue("PB")]
        [Description("Paraíba")]
        Paraiba = 18,

        [DefaultValue("PR")]
        [Description("Paraná")]
        Parana = 19,

        [DefaultValue("PE")]
        [Description("Pernambuco")]
        Pernambuco = 14,

        [DefaultValue("PI")]
        [Description("Piauí")]
        Piaui = 12,

        [DefaultValue("RJ")]
        [Description("Rio de Janeiro")]
        RioDeJaneiro = 2,

        [DefaultValue("RN")]
        [Description("Rio Grande do Norte")]
        RioGrandeDoNorte = 7,

        [DefaultValue("RS")]
        [Description("Rio Grande do Sul")]
        RioGrandeDoSul = 23,

        [DefaultValue("RO")]
        [Description("Rondônia")]
        Rondonia = 6,

        [DefaultValue("RR")]
        [Description("Rorâima")]
        Roraima = 26,

        [DefaultValue("SC")]
        [Description("Santa Catarina")]
        SantaCatarina = 11,

        [DefaultValue("SP")]
        [Description("São Paulo")]
        SaoPaulo = 1,

        [DefaultValue("SE")]
        [Description("Sergipe")]
        Sergipe = 9,

        [DefaultValue("TO")]
        [Description("Tocantins")]
        Tocantins = 5
    }

    public static class UfEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(UfEnum)).Cast<UfEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
