using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class UpdateIndexRepository : BaseRepository, IUpdateIndexRepository {

        public UpdateIndexRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<UpdateIndex>> ListAsync() {
            var query = new StringBuilder()
                .AppendLine("SELECT Id, Nome as Name, CodigoSerieBC as BcCode")
                .AppendLine("FROM UpdateIndex")
                .AppendLine("ORDER BY Nome");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<UpdateIndex>(query.ToString());

            return entity.ToList();
        }
    }
}
