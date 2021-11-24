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
    public class ProposalTypeDao : IProposalTypeDao {
        private readonly IConfiguration configuration;

        public ProposalTypeDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<ProposalTypeEntity> GetAsync(int productExternalCode, int coverageExternalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.Id, a.CdProduto as ProductExternalCode, a.IdProdutoCobertura as CoverageExternalCode, a.Description as Name");
                sbSql.AppendLine("FROM ProposalType a");
                sbSql.AppendLine("WHERE a.CdProduto = @ProductExternalCode");
                sbSql.AppendLine("AND a.IdProdutoCobertura = @CoverageExternalCode");
                var item = await connection.QueryAsync<ProposalTypeEntity>(sbSql.ToString(), new {
                    ProductExternalCode = productExternalCode,
                    CoverageExternalCode = coverageExternalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo tipo da proposta no Marketplace", e);
            }
        }
    }
}
