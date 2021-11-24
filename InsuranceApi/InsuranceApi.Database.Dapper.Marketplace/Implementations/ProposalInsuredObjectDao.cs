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
    public class ProposalInsuredObjectDao : IProposalInsuredObjectDao {
        private readonly IConfiguration configuration;

        public ProposalInsuredObjectDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<ProposalInsuredObjectEntity> GetAsync(int proposalTypeId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.Id, a.ProposalTypeId, a.Text");
                sbSql.AppendLine("FROM ProposalInsuredObject a");
                sbSql.AppendLine("WHERE a.ProposalTypeId = @ProposalTypeId");
                var item = await connection.QueryAsync<ProposalInsuredObjectEntity>(sbSql.ToString(), new {
                    ProposalTypeId = proposalTypeId
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo objeto segurado no Marketplace", e);
            }
        }
    }
}
