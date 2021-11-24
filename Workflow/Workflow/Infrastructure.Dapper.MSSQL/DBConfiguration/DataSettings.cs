using Infrastructure.Interfaces.DBConfiguration;

namespace Infrastructure.Dapper.MSSQL.DBConfiguration {
    public class DataSettings : IDataSettings {
        public string MarketplaceConnection { get; set; }
        public string StageConnection { get; set; }
    }
}