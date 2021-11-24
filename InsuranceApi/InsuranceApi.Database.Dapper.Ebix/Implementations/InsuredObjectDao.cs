using Dapper;
using Microsoft.Extensions.Configuration;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class InsuredObjectDao : IInsuredObjectDao {
        private readonly IConfiguration configuration;

        public InsuredObjectDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<InsuredObjectEntity> GetAsync(int coverageId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT DISTINCT a.InsuredObjectId, a.Name, a.Description, a.ExternalCode, a.Status, a.UserId, a.DateUtc");
                sbSql.AppendLine("  FROM InsuredObject a");
                sbSql.AppendLine("  LEFT JOIN InsuredObjectCoverage b");
                sbSql.AppendLine("    ON b.InsuredObjectId = a.InsuredObjectId");
                sbSql.AppendLine("  LEFT JOIN InsuredObjectLawsuitType c");
                sbSql.AppendLine("    ON c.InsuredObjectId = a.InsuredObjectId");
                sbSql.AppendLine(" WHERE (@CoverageId IS NULL OR b.CoverageId = @CoverageId)");
                sbSql.AppendLine(" ORDER BY a.Status, a.Name");
                var items = await connection.QueryAsync<InsuredObjectEntity>(sbSql.ToString(), new {
                    CoverageId = coverageId,
                });
                return items.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro ao obter objeto segurado", e);
            }
        }

        public async Task<IList<InsuredObjectBlockEntity>> ListBlockAsync(int insuredObjectId, RecordStatusEnum? status) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT InsuredObjectBlockId, InsuredObjectId, PrintMode, Contents, StartInNewLine, PrintOrder, Status, UserId, DateUtc");
                sbSql.AppendLine("  FROM InsuredObjectBlock");
                sbSql.AppendLine(" WHERE InsuredObjectId = @InsuredObjectId");
                sbSql.AppendLine("   AND (@Status IS NULL OR Status = @Status)");
                sbSql.AppendLine(" ORDER BY Status, PrintOrder");
                var items = await connection.QueryAsync<InsuredObjectBlockEntity>(sbSql.ToString(), new {
                    InsuredObjectId = insuredObjectId,
                    Status = (int?)status
                });
                return items.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando blocos do objeto segurado", e);
            }
        }
    }
}
