using Infrastructure.Interfaces.DBConfiguration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Dapper.MSSQL.DBConfiguration {
    public class DatabaseFactory : IDatabaseFactory {
        private IOptions<DataSettings> dataSettings;
        protected string MarketplaceConnectionString => !string.IsNullOrEmpty(dataSettings.Value.MarketplaceConnection) ?
                                                                dataSettings.Value.MarketplaceConnection :
                                                                DatabaseConnection.ConnectionConfiguration.GetConnectionString("MarketplaceConnection");
        protected string StageConnectionString => !string.IsNullOrEmpty(dataSettings.Value.StageConnection) ?
                                                                dataSettings.Value.StageConnection :
                                                                DatabaseConnection.ConnectionConfiguration.GetConnectionString("StageConnection");

        public IDbConnection GetMarketplaceDbConnection => new SqlConnection(MarketplaceConnectionString);
        public IDbConnection GetStageDbConnection => new SqlConnection(StageConnectionString);


        public DatabaseFactory(IOptions<DataSettings> dataSettings) {
            this.dataSettings = dataSettings;
        }
    }
}