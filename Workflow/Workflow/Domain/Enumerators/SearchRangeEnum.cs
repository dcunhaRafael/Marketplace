using System.ComponentModel;

namespace Domain.Enumerators {
    public enum SearchRangeEnum {
        [Description("Últimos 30 dias")]
        [DefaultValue("1")]
        Last30Days = 1,

        [Description("Últimos 60 dias")]
        [DefaultValue("2")]
        Last60Days = 2,

        [Description("Últimos 90 dias")]
        [DefaultValue("3")]
        Last90Days = 3,

        [Description("Período específico")]
        [DefaultValue("4")]
        SpecificPeriod = 4,

        [Description("Ano/mês específico")]
        [DefaultValue("5")]
        SpecificYearMonth = 5,
    }
}
