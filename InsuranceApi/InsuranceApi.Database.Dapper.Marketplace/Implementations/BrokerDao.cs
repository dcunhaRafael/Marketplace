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
    public class BrokerDao : IBrokerDao {
        private readonly IConfiguration configuration;

        public BrokerDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<BrokerEntity> GetAsync(int externalUserId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace")); 
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT BrokerId AS IdCorretor, NULL AS IdPessoa, IdPessoa AS IdExterno, NrCnpjCpf as CpfCnpj");
                sbSql.AppendLine("FROM Broker");
                sbSql.AppendLine("WHERE IdUsuarioCorretor = @externalCode");
                var corretor = await connection.QueryAsync<BrokerEntity>(sbSql.ToString(), new {
                    externalCode = externalUserId
                });
                return corretor.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo corretor no Marketplace", e);
            }
        }
    }
}
