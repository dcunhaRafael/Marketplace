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
    public class BrokerDao : IBrokerDao {
        private readonly IConfiguration configuration;

        public BrokerDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<BrokerEntity> GetAsync(int brokerExternalId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ");
                sbSql.AppendLine("  BrokerId AS IdCorretor,");
                sbSql.AppendLine("  PersonId AS IdPessoa,");
                sbSql.AppendLine("  ExternalId AS IdExterno");
                sbSql.AppendLine("  FROM Broker");
                sbSql.AppendLine(" WHERE ExternalId = @externalCode");
                var corretor = await connection.QueryAsync<BrokerEntity>(sbSql.ToString(), new {
                    externalCode = brokerExternalId
                });
                return corretor.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo corretor", e);
            }
        }
    }
}
