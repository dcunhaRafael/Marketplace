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
    public class ProposalOccurrenceDocumentRepository : BaseRepository, IProposalOccurrenceDocumentRepository {

        public ProposalOccurrenceDocumentRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<ProposalOccurrenceDocument>> ListAsync(long proposalOccurrenceId) {
            var query = new StringBuilder()
                .AppendLine("SELECT pod.ProposalOccurrenceDocumentId, pod.ProposalOccurrenceId, pod.FileName, NULL as FileContents, pod.Status, pod.InclusionUserId, pod.InclusionDate,")
                .AppendLine("   dt.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM ProposalOccurrenceDocument pod")
                .AppendLine("INNER JOIN DocumentType dt ON dt.DocumentTypeId = pod.DocumentTypeId")
                .AppendLine("INNER JOIN Users u ON u.Id = pod.InclusionUserId")
                .AppendLine("WHERE pod.ProposalOccurrenceId = @ProposalOccurrenceId")
                .AppendLine("AND pod.Status = @Active")
                .AppendLine("ORDER BY dt.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<ProposalOccurrenceDocument, DocumentType, User, ProposalOccurrenceDocument>(
                query.ToString(),
                param: new {
                    ProposalOccurrenceId = proposalOccurrenceId,
                    Active = (int)RecordStatusEnum.Active
                },
                map: (otd, dt, u) => {
                    otd.DocumentType = dt;
                    otd.DocumentTypeId = dt.DocumentTypeId ?? 0;
                    otd.InclusionUser = u;
                    return otd;
                },
                splitOn: "DocumentTypeId, UserId"
            );

            return entity.ToList();
        }

        public async Task<ProposalOccurrenceDocument> GetAsync(long proposalOccurrenceDocumentId, bool includeFileContents) {
            var query = new StringBuilder()
                .AppendLine("SELECT pod.ProposalOccurrenceDocumentId, pod.ProposalOccurrenceId, pod.FileName, pod.Status, pod.InclusionUserId, pod.InclusionDate,")
                .AppendLine(includeFileContents ? "pod.FileContents," : "NULL as FileContents,")
                .AppendLine("   dt.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM ProposalOccurrenceDocument pod")
                .AppendLine("INNER JOIN DocumentType dt ON dt.DocumentTypeId = pod.DocumentTypeId")
                .AppendLine("INNER JOIN Users u ON u.Id = pod.InclusionUserId")
                .AppendLine("WHERE pod.ProposalOccurrenceDocumentId = @ProposalOccurrenceDocumentId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<ProposalOccurrenceDocument, DocumentType, User, ProposalOccurrenceDocument>(
                query.ToString(),
                param: new {
                    ProposalOccurrenceDocumentId = proposalOccurrenceDocumentId,
                    Active = (int)RecordStatusEnum.Active
                },
                map: (otd, dt, u) => {
                    otd.DocumentType = dt;
                    otd.DocumentTypeId = dt.DocumentTypeId ?? 0;
                    otd.InclusionUser = u;
                    return otd;
                },
                splitOn: "DocumentTypeId, UserId"
            );

            return entity.FirstOrDefault();
        }

        public async Task<long> AddAsync(ProposalOccurrenceDocument item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO ProposalOccurrenceDocument ( ProposalOccurrenceId, DocumentTypeId, FileName, FileContents, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @ProposalOccurrenceId, @DocumentTypeId, @FileName, @FileContents, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as bigint)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.ProposalOccurrenceId,
                                                          item.DocumentTypeId,
                                                          item.FileName,
                                                          item.FileContents,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateStatusAsync(ProposalOccurrenceDocument item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE ProposalOccurrenceDocument")
                .AppendLine("SET Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE ProposalOccurrenceDocumentId = @ProposalOccurrenceDocumentId");

            using var dbConn = GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.ProposalOccurrenceDocumentId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
        }
    }
}
