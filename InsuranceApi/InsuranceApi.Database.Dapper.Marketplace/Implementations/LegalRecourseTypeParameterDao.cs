using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class LegalRecourseTypeParameterDao : ILegalRecourseTypeParameterDao {
        private readonly IConfiguration configuration;

        public LegalRecourseTypeParameterDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<LegalRecourseTypeParameterEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.Id, a.LegalRecourseTypeId, a.ApeelDepositAmount, a.DateStart, a.DateEnd");
                sbSql.AppendLine("FROM LegalRecourseTypeParameter a");
                sbSql.AppendLine("INNER JOIN LegalRecourseType b ON b.Id = a.LegalRecourseTypeId");
                sbSql.AppendLine("WHERE b.ExternalCode = @ExternalCode");
                sbSql.AppendLine("AND GETDATE() BETWEEN a.DateStart and ISNULL(a.DateEnd, '2078-12-31')");
                var item = await connection.QueryAsync<LegalRecourseTypeParameterEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo parâmetro do tipo de recurso no Marketplace", e);
            }
        }
    }
}
