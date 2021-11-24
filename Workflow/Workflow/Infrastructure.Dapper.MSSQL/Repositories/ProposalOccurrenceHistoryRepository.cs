using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class ProposalOccurrenceHistoryRepository : BaseRepository, IProposalOccurrenceHistoryRepository {

        public ProposalOccurrenceHistoryRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<ProposalOccurrenceHistory>> ListAsync(long proposalOccurrenceId) {
            var query = new StringBuilder()
                .AppendLine("SELECT poh.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM ProposalOccurrenceHistory poh")
                .AppendLine("INNER JOIN Users u ON u.Id = poh.InclusionUserId")
                .AppendLine("WHERE poh.ProposalOccurrenceId = @ProposalOccurrenceId")
                .AppendLine("ORDER BY poh.InclusionDate DESC");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<ProposalOccurrenceHistory, User, ProposalOccurrenceHistory>(
                query.ToString(),
                param: new {
                    ProposalOccurrenceId = proposalOccurrenceId
                },
                map: (otd, u) => {
                    otd.InclusionUser = u;
                    return otd;
                },
                splitOn: "UserId"
            );

            return entity.ToList();
        }

        public async Task<ProposalOccurrenceHistory> GetAsync(int proposalOccurrenceHistoryId) {
            var query = new StringBuilder()
                .AppendLine("SELECT poh.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM ProposalOccurrenceHistory poh")
                .AppendLine("INNER JOIN Users u ON u.Id = poh.InclusionUserId")
                .AppendLine("WHERE poh.ProposalOccurrenceHistoryId = @ProposalOccurrenceHistoryId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<ProposalOccurrenceHistory, User, ProposalOccurrenceHistory>(
                query.ToString(),
                param: new {
                    ProposalOccurrenceHistoryId = proposalOccurrenceHistoryId
                },
                map: (otd, u) => {
                    otd.InclusionUser = u;
                    return otd;
                },
                splitOn: "UserId"
            );

            return entity.FirstOrDefault();
        }

        public async Task AddAsync(ProposalOccurrenceHistory item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO ProposalOccurrenceHistory ( ProposalOccurrenceId, ActionType, Description, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @ProposalOccurrenceId, @ActionType, @Description, @InclusionUserId, @InclusionDate )");

            using var dbConn = base.GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(sbSql.ToString(),
                                                      new {
                                                          item.ProposalOccurrenceId,
                                                          item.ActionType,
                                                          item.Description,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });
        }

    }
}
