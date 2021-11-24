using Infrastructure.Interfaces.DBConfiguration;
using System.Data;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class BaseRepository {
        private readonly IDatabaseFactory databaseOptions;

        public BaseRepository(IDatabaseFactory databaseOptions){
            this.databaseOptions = databaseOptions;
        }

        public IDbConnection GetMarketplaceDbConnection() {
            return databaseOptions.GetMarketplaceDbConnection;
        }

        public IDbConnection GetStageDbConnection() {
            return databaseOptions.GetStageDbConnection;
        }
    }
}
