using Dapper;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class UserDao : IUserDao {
        private readonly IConfiguration configuration;

        public UserDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int?> GetIdAsync(string userName) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("select Id from Users where name = @UserName");
                var item = await connection.QueryAsync<int>(sbSql.ToString(), new {
                    UserName = userName
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo identificador do usuário", e);
            }
        }

    }
}
