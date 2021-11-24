using Dapper;
using Microsoft.Extensions.Configuration;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class RecursalPolicyExpDateDao : IRecursalPolicyExpDateDao {
        private readonly IConfiguration configuration;

        public RecursalPolicyExpDateDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<TermTypeEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT Id, Name, ExternalCode as CodigoExterno");
                sbSql.AppendLine("FROM RecursalPolicyExpDate");
                sbSql.AppendLine("WHERE ExternalCode = @ExternalCode");
                var corretor = await connection.QueryAsync<TermTypeEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return corretor.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo prazo no Marketplace", e);
            }
        }
    }
}
