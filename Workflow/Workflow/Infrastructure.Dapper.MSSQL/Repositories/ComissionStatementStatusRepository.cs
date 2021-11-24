using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class ComissionStatementStatusRepository : BaseRepository, IComissionStatementStatusRepository {

        public ComissionStatementStatusRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<ComissionStatementStatus>> ListAsync() {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM ComissionStatementStatusRepository")
                .AppendLine("ORDER BY Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<ComissionStatementStatus>(query.ToString());

            return entity.ToList();
        }
    }
}
