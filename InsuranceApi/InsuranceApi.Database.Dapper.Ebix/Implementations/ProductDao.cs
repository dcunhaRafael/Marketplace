using Dapper;
using Microsoft.Extensions.Configuration;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class ProductDao : IProductDao {
        private readonly IConfiguration configuration;

        public ProductDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<ProductEntity> GetAsync(string externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProductId AS CodigoProduto,");
                sbSql.AppendLine("  Name as NomeProduto,");
                sbSql.AppendLine("  Description as Descricao,");
                sbSql.AppendLine("  ExternalCode AS CodigoExterno,");
                sbSql.AppendLine("  Status Status");
                sbSql.AppendLine("  FROM Product");
                sbSql.AppendLine(" WHERE ExternalCode = @ExternalCode");
                var item = await connection.QueryAsync<ProductEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo produto", e);
            }
        }
    }
}
