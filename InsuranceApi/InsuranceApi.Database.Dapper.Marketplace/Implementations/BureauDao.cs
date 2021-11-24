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
    public class BureauDao : IBureauDao {
        private readonly IConfiguration configuration;

        public BureauDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<BureauEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.Id, a.Name, a.Code");
                sbSql.AppendLine("FROM Bureau a");
                sbSql.AppendLine("WHERE a.ExternalCode = @ExternalCode");
                var item = await connection.QueryAsync<BureauEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo tribunal no Marketplace", e);
            }
        }
    }
}
