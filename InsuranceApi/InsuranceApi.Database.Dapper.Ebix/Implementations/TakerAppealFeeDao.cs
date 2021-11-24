using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class TakerAppealFeeDao : ITakerAppealFeeDao {
        private readonly IConfiguration configuration;

        public TakerAppealFeeDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<TakerAppealFeeEntity> GetAsync(int takerExternalCode, int productId, int coverageId, decimal insuredAmountValue, int termTypeId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.TakerAppealFeeId, a.TakerId, a.ProductId, a.CoverageId, a.InsuredAmountValueMin, a.InsuredAmountValueMax, a.TermTypeId, a.PremiumValue, a.Status, a.UserId, a.DateUtc");
                sbSql.AppendLine("     , b.ExternalId as TakerExternalId");
                sbSql.AppendLine("     , c.Name as TakerName, CAST(c.Document as numeric(14)) as TakerDocument");
                sbSql.AppendLine("     , d.Name as ProductName");
                sbSql.AppendLine("     , e.Name as CoverageName");
                sbSql.AppendLine("  FROM TakerAppealFee a");
                sbSql.AppendLine(" INNER JOIN Taker b");
                sbSql.AppendLine("    ON b.TakerId = a.TakerId");
                sbSql.AppendLine(" INNER JOIN Person c");
                sbSql.AppendLine("    ON c.PersonId = b.PersonId");
                sbSql.AppendLine(" INNER JOIN Product d");
                sbSql.AppendLine("    ON d.ProductId = a.ProductId");
                sbSql.AppendLine(" INNER JOIN Coverage e");
                sbSql.AppendLine("    ON e.CoverageId = a.CoverageId");
                sbSql.AppendLine(" WHERE b.ExternalId = @TakerExternalId");
                sbSql.AppendLine("   AND d.ProductId = @ProductId");
                sbSql.AppendLine("   AND e.CoverageId = @CoverageId");
                sbSql.AppendLine("   AND @InsuredAmountValue >= a.InsuredAmountValueMin");
                sbSql.AppendLine("   AND @InsuredAmountValue <= a.InsuredAmountValueMax");
                sbSql.AppendLine("   AND a.TermTypeId = @TermTypeId");
                sbSql.AppendLine("   AND a.Status = 1");
                var item = await connection.QueryAsync<TakerAppealFeeEntity>(sbSql.ToString(),
                                                            new {
                                                                TakerExternalId = takerExternalCode,
                                                                ProductId = productId,
                                                                CoverageId = coverageId,
                                                                InsuredAmountValue = insuredAmountValue,
                                                                TermTypeId = termTypeId
                                                            });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo taxa recursal", e);
            }
        }
    }
}
