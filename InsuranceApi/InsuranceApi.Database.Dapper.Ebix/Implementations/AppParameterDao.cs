using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class AppParameterDao : IAppParameterDao {
        private readonly IConfiguration configuration;

        public AppParameterDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IList<ParametroEntity>> ListAsync() {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT AppParameterId, Name, Description, Value, UserId, DateUtc");
                sbSql.AppendLine("  FROM AppParameter");
                var item = await connection.QueryAsync<ParametroEntity>(sbSql.ToString());
                return item.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando parâmetros", e);
            }
        }

        public async Task<ParametroEntity> GetAsync(AppParameterEnum appParameter) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT AppParameterId, Name, Description, Value, UserId, DateUtc");
                sbSql.AppendLine("  FROM AppParameter");
                sbSql.AppendLine(" WHERE AppParameterId = @AppParameterId");
                var item = await connection.QueryAsync<ParametroEntity>(sbSql.ToString(), new {
                    AppParameterId = appParameter
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro ao obter parâmetro", e);
            }
        }
    }
}
