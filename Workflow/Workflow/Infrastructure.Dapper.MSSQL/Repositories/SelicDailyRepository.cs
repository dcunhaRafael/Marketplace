using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class SelicDailyRepository : BaseRepository, ISelicDailyRepository {

        public SelicDailyRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<SelicDaily>> ListAsync(DateTime fromDate, DateTime toDate) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM SelicDaily")
                .AppendLine("WHERE Date >= @FromDate AND Date <= @ToDate")
                .AppendLine("ORDER BY Date");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<SelicDaily>(
                query.ToString(),
                param: new { FromDate = fromDate, ToDate = toDate }
            );

            return entity.ToList();
        }

        public async Task SaveAsync(SelicDaily item) {
            using var dbConn = base.GetMarketplaceDbConnection();
            var sbSql = new StringBuilder("UPDATE SelicDaily SET Value = @Value WHERE Date = @Date");
            var recordAffected = await dbConn.ExecuteAsync(sbSql.ToString(), new { item.Date, item.Value });
            if (recordAffected == 0) {
                sbSql = new StringBuilder("INSERT INTO SelicDaily ( Date, Value ) VALUES ( @Date, @Value )");
                await dbConn.ExecuteAsync(sbSql.ToString(), new { item.Date, item.Value });
            }
        }
    }
}
