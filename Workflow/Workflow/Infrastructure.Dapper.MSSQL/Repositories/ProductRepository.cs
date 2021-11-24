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
    public class ProductRepository : BaseRepository, IProductRepository {

        public ProductRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<Product>> ListAsync(RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT p.ProductId, p.NmProduto as Name, p.CdProduto as LegacyCode ")
                .AppendLine("FROM Product p")
                .AppendLine("ORDER BY p.NmProduto");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Product>(
                query.ToString(),
                param: new { Status = (int?)status }
            );

            return entity.ToList();
        }
    }
}
