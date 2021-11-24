using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using InsuranceApi.Domain.Common.Exceptions;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class RateDao : BaseDao, IRateDao {
        private readonly IConfiguration configuration;

        public RateDao(IConfiguration configuration, IAppAuditorshipDao appAuditorshipDao) : base(appAuditorshipDao) {
            this.configuration = configuration;
        }

        public async Task<RateEntity> GetAsync(decimal insuredAmountValue, int termTypeId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.AppealFeeId, a.InsuredAmountValueMin, a.InsuredAmountValueMax, a.TermTypeId, a.PremiumValue, a.Status, a.UserId, a.DateUtc");
                sbSql.AppendLine("  FROM AppealFee a");
                sbSql.AppendLine(" INNER JOIN TermType b");
                sbSql.AppendLine("    ON b.TermTypeId = a.TermTypeId");
                sbSql.AppendLine(" WHERE @InsuredAmountValue >= a.InsuredAmountValueMin");
                sbSql.AppendLine("   AND @InsuredAmountValue <= a.InsuredAmountValueMax");
                sbSql.AppendLine("   AND a.TermTypeId = @TermTypeId");
                sbSql.AppendLine("   AND a.Status = 1");
                var item = await connection.QueryAsync<RateEntity>(sbSql.ToString(), new {
                    InsuredAmountValue = Math.Round(insuredAmountValue, termTypeId),
                    TermTypeId = termTypeId
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo taxa", e);
            }
        }
    }
}
