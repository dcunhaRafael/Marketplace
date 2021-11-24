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
    public class ProductDao : IProductDao {
        private readonly IConfiguration configuration;

        public ProductDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<ProductEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProductId as CodigoProduto, CdProduto as CodigoExterno, NmProduto as NomeProduto, LegacyId");
                sbSql.AppendLine("FROM Product");
                sbSql.AppendLine("WHERE CdProduto = @ExternalCode");
                var item = await connection.QueryAsync<ProductEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo produto no Marketplace", e);
            }
        }
    }
}
