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
    public class CommercialEntityStructureDao : ICommercialEntityStructureDao {
        private readonly IConfiguration configuration;

        public CommercialEntityStructureDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<CommercialEntityStructure> GetAsync(long cpfCnpj) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT Id, Name, CommercialStructureId");
                sbSql.AppendLine("FROM CommercialEntityStructure");
                sbSql.AppendLine("WHERE CAST(REPLACE(REPLACE(REPLACE(CpfOrCnpj, '.', ''), '/', ''), '-', '') as bigint) = @CpfCnpj");
                var item = await connection.QueryAsync<CommercialEntityStructure>(sbSql.ToString(), new {
                    CpfCnpj = cpfCnpj
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo estrutura comercial no Marketplace", e);
            }
        }

    }
}
