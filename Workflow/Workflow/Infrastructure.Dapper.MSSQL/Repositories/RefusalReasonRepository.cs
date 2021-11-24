using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class RefusalReasonRepository : BaseRepository, IRefusalReasonRepository {

        public RefusalReasonRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<RefusalReason>> ListAsync(RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM RefusalReason")
                .AppendLine("WHERE Status = @Status")
                .AppendLine("ORDER BY Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<RefusalReason>(
                query.ToString(),
                param: new { Status = (int?)status }
            );

            return entity.ToList();
        }

        public async Task<RefusalReason> GetAsync(int refusalReasonId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM RefusalReason")
                .AppendLine("WHERE RefusalReasonId = @RefusalReasonId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<RefusalReason>(
                query.ToString(),
                param: new { RefusalReasonId = refusalReasonId }
            );

            return entity.FirstOrDefault();
        }
    }
}
