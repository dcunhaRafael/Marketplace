
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
    public class PolicyBatchConfigurationMailRepository : BaseRepository, IPolicyBatchConfigurationMailRepository {
        private readonly IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository;

        public PolicyBatchConfigurationMailRepository(IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository,
            IDatabaseFactory databaseOptions) : base(databaseOptions) {
            this.policyBatchConfigurationMailDestinationRepository = policyBatchConfigurationMailDestinationRepository;
        }

        public async Task<IList<PolicyBatchConfigurationMail>> ListAsync(int policyBatchConfigurationId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatchConfigurationMail")
                .AppendLine("WHERE PolicyBatchConfigurationId = @PolicyBatchConfigurationId")
                .AppendLine("ORDER BY InclusionDate desc");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatchConfigurationMail>(
                query.ToString(),
                param: new {
                    PolicyBatchConfigurationId = policyBatchConfigurationId,
                });

            return entity.ToList();
        }

        public async Task<PolicyBatchConfigurationMail> GetAsync(int policyBatchConfigurationMailId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatchConfigurationMail")
                .AppendLine("WHERE PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var task = await dbConn.QueryAsync<PolicyBatchConfigurationMail>(
                query.ToString(),
                param: new {
                    PolicyBatchConfigurationMailId = policyBatchConfigurationMailId
                });

            var entity = task.FirstOrDefault();
            if (entity != null) {
                entity.Destinations = await policyBatchConfigurationMailDestinationRepository.ListAsync(policyBatchConfigurationMailId);
            }

            return entity;
        }

        public async Task<int> AddAsync(PolicyBatchConfigurationMail item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO PolicyBatchConfigurationMail ( PolicyBatchConfigurationId, DaysBeforeExpiration, Subject, Body, SendToBroker, SendToSubscription, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @PolicyBatchConfigurationId, @DaysBeforeExpiration, @Subject, @Body, @SendToBroker, @SendToSubscription, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.PolicyBatchConfigurationId,
                                                          item.DaysBeforeExpiration,
                                                          item.Subject,
                                                          item.Body,
                                                          item.SendToBroker,
                                                          item.SendToSubscription,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(PolicyBatchConfigurationMail item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfigurationMail")
                .AppendLine("SET DaysBeforeExpiration = @DaysBeforeExpiration, ")
                .AppendLine("   Subject = @Subject, ")
                .AppendLine("   Body = @Body, ")
                .AppendLine("   SendToBroker = @SendToBroker, ")
                .AppendLine("   SendToSubscription = @SendToSubscription, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchConfigurationMailId,
                    item.DaysBeforeExpiration,
                    item.Subject,
                    item.Body,
                    item.SendToBroker,
                    item.SendToSubscription,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusAsync(PolicyBatchConfigurationMail item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatchConfigurationMail")
                .AppendLine("SET Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchConfigurationMailId = @PolicyBatchConfigurationMailId");

            using var dbConn = GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchConfigurationMailId,
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
