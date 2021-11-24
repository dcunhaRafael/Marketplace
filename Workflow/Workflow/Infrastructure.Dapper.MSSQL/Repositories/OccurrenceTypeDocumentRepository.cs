using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class OccurrenceTypeDocumentRepository : BaseRepository, IOccurrenceTypeDocumentRepository {

        public OccurrenceTypeDocumentRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<OccurrenceTypeDocument>> ListAsync(int occurrenceTypeId) {
            var query = new StringBuilder()
                .AppendLine("SELECT otd.OccurrenceTypeDocumentId, otd.OccurrenceTypeId, otd.IsRequired, dt.*")
                .AppendLine("FROM OccurrenceTypeDocument otd")
                .AppendLine("INNER JOIN DocumentType dt ON dt.DocumentTypeId = otd.DocumentTypeId")
                .AppendLine("WHERE otd.OccurrenceTypeId = @OccurrenceTypeId")
                .AppendLine("AND otd.Status = @Active")
                .AppendLine("ORDER BY dt.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<OccurrenceTypeDocument, DocumentType, OccurrenceTypeDocument>(
                query.ToString(),
                param: new { 
                    OccurrenceTypeId = occurrenceTypeId, 
                    Active = (int)RecordStatusEnum.Active
                },
                map: (otd, dt) => {
                    otd.DocumentType = dt;
                    otd.DocumentTypeId = dt.DocumentTypeId;
                    return otd;
                },
                splitOn: "DocumentTypeId"
            );

            return entity.ToList();
        }

        public async Task AddAsync(OccurrenceTypeDocument item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO OccurrenceTypeDocument ( OccurrenceTypeId, DocumentTypeId, IsRequired, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @OccurrenceTypeId, @DocumentTypeId, @IsRequired, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(sbSql.ToString(),
                                                      new {
                                                          item.OccurrenceTypeId,
                                                          item.DocumentTypeId,
                                                          item.IsRequired,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });
        }

        public async Task UpdateAsync(OccurrenceTypeDocument item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE OccurrenceTypeDocument")
                .AppendLine("SET OccurrenceTypeId = @OccurrenceTypeId, ")
                .AppendLine("   DocumentTypeId = @DocumentTypeId, ")
                .AppendLine("   IsRequired = @IsRequired, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE OccurrenceTypeDocumentId = @OccurrenceTypeDocumentId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.OccurrenceTypeDocumentId,
                    item.OccurrenceTypeId,
                    item.DocumentTypeId,
                    item.IsRequired,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusNotInAsync(OccurrenceType master, IList<int> ids) {
            var query = new StringBuilder()
                .AppendLine("UPDATE OccurrenceTypeDocument")
                .AppendLine("SET Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE OccurrenceTypeId = @OccurrenceTypeId AND DocumentTypeId NOT IN @NotInIds AND Status = @Active");

            using var dbConn = GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    NotInIds = ids.ToArray(),
                    master.OccurrenceTypeId,
                    master.Status,
                    master.LastChangeUserId,
                    master.LastChangeDate,
                    Active = (int)RecordStatusEnum.Active
                });
        }
    }
}
