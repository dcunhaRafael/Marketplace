using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class LegalRecourseTypeDao : ILegalRecourseTypeDao {
        private readonly IConfiguration configuration;

        public LegalRecourseTypeDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IList<LegalRecourseTypeEntity>> ListAsync() {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT LegalRecourseTypeId, Name, Status, ExternalCode, UserId, DateUtc");
                sbSql.AppendLine("  FROM LegalRecourseType ");
                sbSql.AppendLine(" WHERE status = 1");
                sbSql.AppendLine(" ORDER BY LegalRecourseTypeId DESC");
                var result = await connection.QueryAsync<LegalRecourseTypeEntity>(sbSql.ToString());
                return result.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando tipos de recurso", e);
            }
        }

        public async Task<LegalRecourseTypeEntity> GetAsync(int codigo) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT LegalRecourseTypeId, Name, Status, ExternalCode, UserId, DateUtc");
                sbSql.AppendLine("  FROM LegalRecourseType ");
                sbSql.AppendLine(" WHERE LegalRecourseTypeId = @LegalRecourseTypeId");
                sbSql.AppendLine(" AND status = 1");
                sbSql.AppendLine(" ORDER BY LegalRecourseTypeId DESC");
                var item = await connection.QueryAsync<LegalRecourseTypeEntity>(sbSql.ToString(),
                                    new {
                                        LegalRecourseTypeId = codigo
                                    });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo tipo de recurso", e);
            }
        }

        public async Task<RecursoParametroEntity> GetParameterAsync(int codigo) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ");
                sbSql.AppendLine("    LegalRecourseTypeId AS IdTipoRecurso,");
                sbSql.AppendLine("    LegalRecourseTypeParameterId AS IdTipoRecursoParametro,");
                sbSql.AppendLine("    ApeelDepositAmount  AS ValorDepositoRecursal");
                sbSql.AppendLine("  FROM LegalRecourseTypeParameter ");
                sbSql.AppendLine(" WHERE LegalRecourseTypeId = @LegalRecourseTypeId");
                sbSql.AppendLine(" ORDER BY LegalRecourseTypeId DESC");
                var item = await connection.QueryAsync<RecursoParametroEntity>(sbSql.ToString(), new {
                    LegalRecourseTypeId = codigo
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo parâmetros do tipo de recurso", e);
            }
        }
    }
}