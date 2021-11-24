
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class AppServiceLogRepository : BaseRepository, IAppServiceLogRepository {

        public AppServiceLogRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task AddAsync(AppServiceLog item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO AppServiceLog ( AppServiceId, InclusionDate, LogLevel, Message )")
                .AppendLine("  VALUES ( @AppServiceId, @InclusionDate, @LogLevel, @Message)");

            using var dbConn = base.GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(sbSql.ToString(),
                                        new {
                                            item.AppServiceId,
                                            InclusionDate = DateTime.Now,
                                            item.LogLevel,
                                            item.Message
                                        });
        }

    }
}
