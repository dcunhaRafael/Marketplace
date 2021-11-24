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
    public class ProfileRepository : BaseRepository, IProfileRepository {

        public ProfileRepository(IDatabaseFactory databaseOptions): base(databaseOptions) { }

        public async Task<IList<Profile>> ListAsync(RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT p.Id as ProfileId, p.Description as Name")
                .AppendLine("FROM Profile p")
                .AppendLine("WHERE p.Active = @Status")
                .AppendLine("ORDER BY p.Description");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Profile>(
                query.ToString(),
                param: new { Status = (int?)status }
            );

            return entity.ToList();
        }
    }
}
