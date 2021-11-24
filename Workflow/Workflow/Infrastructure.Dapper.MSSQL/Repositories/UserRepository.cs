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
    public class UserRepository : BaseRepository, IUserRepository {

        public UserRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<User>> ListAsync(int profileId, RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM Users u")
                .AppendLine("WHERE u.ProfileId = @ProfileId")
                .AppendLine("AND u.Active = @Status")
                .AppendLine("ORDER BY u.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<User>(
                query.ToString(),
                param: new { ProfileId = profileId, Status = (int?)status }
            );

            return entity.ToList();
        }

        public async Task<User> GetAsync(int userId) {
            var query = new StringBuilder()
                .AppendLine("SELECT u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM Users u")
                .AppendLine("WHERE u.Id = @UserId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<User>(
                query.ToString(),
                param: new { UserId = userId }
            );

            return entity.FirstOrDefault();
        }
    }
}
