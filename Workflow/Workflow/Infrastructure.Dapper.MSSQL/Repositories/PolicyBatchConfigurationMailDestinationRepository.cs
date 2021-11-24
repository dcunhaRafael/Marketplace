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
    public class PolicyBatchConfigurationMailDestinationRepository : BaseRepository, IPolicyBatchConfigurationMailDestinationRepository {

        public PolicyBatchConfigurationMailDestinationRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<PolicyBatchConfigurationMailDestination>> ListAsync(int policyBatchConfigurationMailId) {
            var query = new StringBuilder()
                .AppendLine("SELECT olu.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM PolicyBatchConfigurationMailDestination olu")
                .AppendLine("INNER JOIN Users u ON U.Id = olu.UserId")
                .AppendLine("WHERE olu.PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId")
                .AppendLine("AND olu.Status = @Active")
                .AppendLine("ORDER BY u.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatchConfigurationMailDestination, User, PolicyBatchConfigurationMailDestination>(
                query.ToString(),
                param: new { PolicyBatchConfigurationMailId = policyBatchConfigurationMailId, Active = (int)RecordStatusEnum.Active },
                map: (olu, u) => {
                    olu.User = u;
                    return olu;
                },
                splitOn: "UserId");

            return entity.ToList();
        }

        public async Task AddAsync(PolicyBatchConfigurationMailDestination item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO PolicyBatchConfigurationMailDestination ( PolicyBatchConfigurationMailId, UserId, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @PolicyBatchConfigurationMailId, @UserId, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(sbSql.ToString(),
                                                      new {
                                                          item.PolicyBatchConfigurationMailId,
                                                          item.UserId,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });
        }

        public async Task UpdateAsync(PolicyBatchConfigurationMailDestination item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfigurationMailDestination")
                .AppendLine("SET PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId, ")
                .AppendLine("   UserId = @UserId, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationMailDestinationId = @PolicyBatchConfigurationMailDestinationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchConfigurationMailDestinationId,
                    item.PolicyBatchConfigurationMailId,
                    item.UserId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusNotInAsync(PolicyBatchConfigurationMail master, IList<int> ids) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfigurationMailDestination")
                .AppendLine("SET Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId AND UserId NOT IN @NotInIds AND Status = @Active");

            using var dbConn = GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    NotInIds = ids.ToArray(),
                    master.PolicyBatchConfigurationMailId,
                    master.Status,
                    master.LastChangeUserId,
                    master.LastChangeDate,
                    Active = (int)RecordStatusEnum.Active
                });
        }
    }
}
