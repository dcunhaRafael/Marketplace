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
    public class DocumentTypeRepository : BaseRepository, IDocumentTypeRepository {

        public DocumentTypeRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<DocumentType>> ListAsync(RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT dt.*")
                .AppendLine("FROM DocumentType dt")
                .AppendLine("WHERE dt.Status = @Status")
                .AppendLine("ORDER BY dt.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<DocumentType>(
                query.ToString(),
                param: new { Status = (int?)status }
            );

            return entity.ToList();
        }

        public async Task<DocumentType> GetAsync(int documentTypeId) {
            var query = new StringBuilder()
                .AppendLine("SELECT dt.*")
                .AppendLine("FROM DocumentType dt")
                .AppendLine("WHERE dt.DocumentTypeId = @DocumentTypeId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<DocumentType>(
                query.ToString(),
                param: new { DocumentTypeId = documentTypeId }
            );

            return entity.FirstOrDefault();
        }
    }
}
