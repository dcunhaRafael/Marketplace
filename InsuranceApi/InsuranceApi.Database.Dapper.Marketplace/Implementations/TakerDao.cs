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
    public class TakerDao : ITakerDao {
        private readonly IConfiguration configuration;

        public TakerDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<TakerEntity> GetAsync(long cpfCnpj) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT Id as TakerId, IdPessoa as ExternalId");
                sbSql.AppendLine("FROM Taker");
                sbSql.AppendLine("WHERE CAST(NrCnpjCpf as bigint) = @CpfCnpj");
                var item = await connection.QueryAsync<TakerEntity>(sbSql.ToString(), new {
                    CpfCnpj = cpfCnpj
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo empresa no Marketplace", e);
            }
        }

    }
}
