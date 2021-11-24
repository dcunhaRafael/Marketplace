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
    public class CoverageRepository : BaseRepository, ICoverageRepository {

        public CoverageRepository(IDatabaseFactory databaseOptions): base(databaseOptions) { }

        public async Task<IList<Coverage>> ListAsync(int productId, RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT c.CoverageId, c.NmCobertura as Name, c.IdProdutoCobertura as LegacyCode ")
                .AppendLine("FROM Coverage c")
                .AppendLine("WHERE c.ProductId = @ProductId")
                .AppendLine("ORDER BY c.NmCobertura");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Coverage>(
                query.ToString(),
                param: new { ProductId = productId, Status = (int?)status, Active = (int)RecordStatusEnum.Active }
            );

            return entity.ToList();
        }

    }
}
