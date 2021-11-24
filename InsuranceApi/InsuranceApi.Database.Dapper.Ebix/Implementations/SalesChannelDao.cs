using Dapper;
using Microsoft.Extensions.Configuration;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class SalesChannelDao : ISalesChannelDao {
        private readonly IConfiguration configuration;

        public SalesChannelDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<SalesChannelEntity>GetAsync(int productId, long personId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT TOP 1 a.SalesChannelId, a.Name, a.Status, a.StartValidaty, a.EndValidaty, a.IsBiddingDefault, a.IsByRegion, a.UserId, a.DateUtc");
                sbSql.AppendLine("  FROM SalesChannel a");
                sbSql.AppendLine(" INNER JOIN SalesChannelProduct b");
                sbSql.AppendLine("    ON b.SalesChannelId = a.SalesChannelId");
                sbSql.AppendLine("   AND b.ProductId = @ProductId");
                sbSql.AppendLine("   AND b.Status = @Status");
                sbSql.AppendLine(" INNER JOIN SalesChannelPerson c");
                sbSql.AppendLine("    ON c.SalesChannelId = a.SalesChannelId");
                sbSql.AppendLine("   AND (c.PersonId = @PersonId OR");
                sbSql.AppendLine("        c.PersonId = (SELECT TOP 1 ParentId FROM SalesChannelPerson WHERE SalesChannelId = a.SalesChannelId AND PersonId = @PersonId))");
                sbSql.AppendLine("   AND c.EntityTypeId = @EntityTypeId");
                sbSql.AppendLine("   AND c.Status = @Status");
                sbSql.AppendLine(" WHERE a.StartValidaty < getdate()");
                sbSql.AppendLine("   AND (a.EndValidaty IS NULL OR a.EndValidaty > getdate())");
                sbSql.AppendLine("   AND a.Status = @Status");
                var item = await connection.QueryAsync<SalesChannelEntity>(sbSql.ToString(),
                                                            new {
                                                                ProductId = productId,
                                                                Status = (int)RecordStatusEnum.Ativo,
                                                                PersonId = personId,
                                                                EntityTypeId = (int)EntityTypeEnum.Produtor
                                                            });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo canal de venda padrão do produtor", e);
            }
        }
    }
}
