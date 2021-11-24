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
    public class ProposalStatusDao : IProposalStatusDao {
        private readonly IConfiguration configuration;

        public ProposalStatusDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<ProposalStatusEntity> GetAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.Id, a.Code as ExternalCode, a.Description as Name");
                sbSql.AppendLine("FROM ProposalStatus a");
                sbSql.AppendLine("WHERE CAST(a.Code AS INT) = @ExternalCode");
                var item = await connection.QueryAsync<ProposalStatusEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo status da proposta no Marketplace", e);
            }
        }
    }
}
