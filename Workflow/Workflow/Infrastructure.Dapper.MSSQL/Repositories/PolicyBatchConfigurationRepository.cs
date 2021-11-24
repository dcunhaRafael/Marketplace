
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class PolicyBatchConfigurationRepository : BaseRepository, IPolicyBatchConfigurationRepository {

        public PolicyBatchConfigurationRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) {

        }

        public async Task<IList<PolicyBatchConfiguration>> ListAsync(PolicyBatchConfiguration filters) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatchConfiguration")
                .AppendLine("WHERE (@BatchType IS NULL OR BatchType = @BatchType)")
                .AppendLine("AND (@Status IS NULL OR Status = @Status)")
                .AppendLine("ORDER BY InclusionDate desc");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatchConfiguration>(
                query.ToString(),
                param: new {
                    filters.BatchType,
                    filters.Status
                });

            return entity.ToList();
        }

        public async Task<PolicyBatchConfiguration> GetAsync(int policyBatchConfigurationId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatchConfiguration")
                .AppendLine("WHERE PolicyBatchConfigurationId = @PolicyBatchConfigurationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatchConfiguration>(
                query.ToString(),
                param: new {
                    PolicyBatchConfigurationId = policyBatchConfigurationId
                });

            return entity.FirstOrDefault();
        }

        public async Task<int> AddAsync(PolicyBatchConfiguration item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO PolicyBatchConfiguration ( BatchType, GroupByBroker, GroupByTaker, GroupByInsured, ProcessDays, CompulsoryIssueDays, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @BatchType, @GroupByBroker, @GroupByTaker, @GroupByInsured, @ProcessDays, @CompulsoryIssueDays, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.BatchType,
                                                          item.GroupByBroker,
                                                          item.GroupByTaker,
                                                          item.GroupByInsured,
                                                          item.ProcessDays,
                                                          item.CompulsoryIssueDays,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(PolicyBatchConfiguration item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfiguration")
                .AppendLine("SET BatchType = @BatchType, ")
                .AppendLine("   GroupByBroker = @GroupByBroker, ")
                .AppendLine("   GroupByTaker = @GroupByTaker, ")
                .AppendLine("   GroupByInsured = @GroupByInsured, ")
                .AppendLine("   ProcessDays = @ProcessDays, ")
                .AppendLine("   CompulsoryIssueDays = @CompulsoryIssueDays, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationId = @PolicyBatchConfigurationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchConfigurationId,
                    item.BatchType,
                    item.GroupByBroker,
                    item.GroupByTaker,
                    item.GroupByInsured,
                    item.ProcessDays,
                    item.CompulsoryIssueDays,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusAsync(PolicyBatchConfiguration item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfiguration")
                .AppendLine("SET Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationId = @PolicyBatchConfigurationId");

            using var dbConn = GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchConfigurationId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }
    }
}
