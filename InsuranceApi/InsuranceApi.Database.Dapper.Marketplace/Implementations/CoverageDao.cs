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
    public class CoverageDao : ICoverageDao {
        private readonly IConfiguration configuration;

        public CoverageDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<CoverageEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT CoverageId as IdCobertura, NmCobertura as NomeCobertura, IdProdutoCobertura as ExternalCode");
                sbSql.AppendLine("FROM Coverage");
                sbSql.AppendLine("WHERE IdProdutoCobertura = @ExternalCode");
                var item = await connection.QueryAsync<CoverageEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo cobertura no Marketplace", e);
            }
        }
    }
}
