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
    public class TermTypeDao : ITermTypeDao {
        private readonly IConfiguration configuration;

        public TermTypeDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IList<TermTypeEntity>> ListAsync(int coverageExternalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT DISTINCT  ");
                sbSql.AppendLine("	    a.TermSize, ");
                sbSql.AppendLine("	    a.TermUnit, ");
                sbSql.AppendLine("	    a.Status, ");
                sbSql.AppendLine("	    a.UserId, ");
                sbSql.AppendLine("	    a.TermTypeId AS Id, ");
                sbSql.AppendLine("	    a.Name  ");
                sbSql.AppendLine("	FROM TermType a ");
                sbSql.AppendLine("	INNER JOIN TermTypeCoverage c on c.TermTypeId = a.TermTypeId ");
                sbSql.AppendLine("	INNER JOIN coverage  b on c.CoverageId = b.CoverageId ");
                sbSql.AppendLine("WHERE  b.ExternalCode = @CoverageId ");
                sbSql.AppendLine("AND c.Status = 1 AND b.Status = 1 AND a.Status = 1 ");
                var item = await connection.QueryAsync<TermTypeEntity>(sbSql.ToString(), new {
                    CoverageId = coverageExternalCode
                });
                return item.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando prazos", e);
            }
        }

        public async Task<TermTypeEntity> GetAsync(int termTypeId, int coverageExternalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT DISTINCT      ");
                sbSql.AppendLine("	    a.TermSize, ");
                sbSql.AppendLine("	    a.TermUnit, ");
                sbSql.AppendLine("	    a.Status, ");
                sbSql.AppendLine("	    a.UserId, ");
                sbSql.AppendLine("	    a.TermTypeId AS Id, ");
                sbSql.AppendLine("	    a.Name  ");
                sbSql.AppendLine("	FROM TermType a ");
                sbSql.AppendLine("	INNER JOIN TermTypeCoverage c on c.TermTypeId = a.TermTypeId ");
                sbSql.AppendLine("	INNER JOIN coverage  b on c.CoverageId = b.CoverageId ");
                sbSql.AppendLine("WHERE  a.TermTypeId = @CodigoPrazo ");
                sbSql.AppendLine("AND  b.ExternalCode = @CoverageId ");
                sbSql.AppendLine("AND c.Status = 1 AND b.Status = 1 AND a.Status = 1 ");

                var item = await connection.QueryAsync<TermTypeEntity>(sbSql.ToString(), new {
                    CodigoPrazo = termTypeId,
                    CoverageId = coverageExternalCode
                });

                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo prazo", e);
            }
        }
    }
}
