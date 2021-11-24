using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
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
    public class CoverageDao : ICoverageDao {
        private readonly IConfiguration configuration;

        public CoverageDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IList<CoverageEntity>> ListAsync(int? productId, RecordStatusEnum status) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.CoverageId as IdCobertura, a.Name as NomeCobertura, a.Description, a.ExternalCode, a.InsuredInputType, a.LawsuitInputType, a.DisplayRequesterInformation, a.DisplayLawsuitTypeInformation");
                sbSql.AppendLine("     , a.DefaultPaymentFormId, a.DefaultPaymentInstallmentId, a.DefaultPaymentFrequencyId, a.DefaultTermTypeId, a.DefaultPaymentDay, a.IsBillingDataLocked");
                sbSql.AppendLine("     , a.Status, a.UserId, a.DateUtc");
                sbSql.AppendLine("  FROM Coverage a");
                sbSql.AppendLine("  LEFT JOIN CoverageProduct b");
                sbSql.AppendLine("    ON b.CoverageId = a.CoverageId");
                sbSql.AppendLine("   AND b.Status = 1");
                sbSql.AppendLine(" WHERE (@ProductId IS NULL OR b.ProductId = @ProductId)");
                sbSql.AppendLine("   AND (@Status IS NULL OR a.Status = @Status)");
                sbSql.AppendLine(" ORDER BY a.Status, a.Name");
                var items = await connection.QueryAsync<CoverageEntity>(sbSql.ToString(), new {
                    ProductId = productId,
                    Status = status
                });
                return items.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando coberturas", e);
            }
        }

        public async Task<CoverageEntity> GetAsync(string productExternalCode, string coverageExternalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("select   ");
                sbSql.AppendLine("		c.CoverageId as IdCobertura, ");
                sbSql.AppendLine("		c.Name as NomeCobertura, ");
                sbSql.AppendLine("		c.Description, ");
                sbSql.AppendLine("		c.ExternalCode, ");
                sbSql.AppendLine("		c.DisplayRequesterInformation, ");
                sbSql.AppendLine("		c.DefaultPaymentFormId,  ");
                sbSql.AppendLine("		c.DefaultPaymentInstallmentId, ");
                sbSql.AppendLine("		c.DefaultPaymentFrequencyId, ");
                sbSql.AppendLine("		c.DefaultTermTypeId, ");
                sbSql.AppendLine("		c.DefaultPaymentDay, ");
                sbSql.AppendLine("		c.IsBillingDataLocked  ");
                sbSql.AppendLine("	from Product a ");
                sbSql.AppendLine("	inner join CoverageProduct b ");
                sbSql.AppendLine("		on	a.ProductId  = b.ProductId ");
                sbSql.AppendLine("	Inner join Coverage c ");
                sbSql.AppendLine("	on c.CoverageId = b.CoverageId ");
                sbSql.AppendLine("where a.ExternalCode  = @ExternoProduto");
                sbSql.AppendLine("and c.ExternalCode = @ExternoModalidade ");
                sbSql.AppendLine("and c.Status = 1 ");
                var item = await connection.QueryAsync<CoverageEntity>(sbSql.ToString(), new {
                    ExternoProduto = productExternalCode,
                    ExternoModalidade = coverageExternalCode
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo cobertura", e);
            }
        }

        public async Task<CoberturaAgravoEntity> GetGrievanceAsync(int externalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT a.CoverageId, a.HasGrievance, a.IsRequired, a.GrievanceType, a.DefaultValue, a.MinValue, a.MaxValue, a.IsLocked, a.Status, a.UserId, a.DateUtc");
                sbSql.AppendLine("  FROM CoverageGrievance a");
                sbSql.AppendLine(" INNER JOIN Coverage b");
                sbSql.AppendLine("    ON b.CoverageId = a.CoverageId");
                sbSql.AppendLine(" WHERE b.ExternalCode = CAST(@ExternalCode AS VARCHAR(50))");
                var items = await connection.QueryAsync<CoberturaAgravoEntity>(sbSql.ToString(), new {
                    ExternalCode = externalCode
                });
                return items.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo parâmetros da cobertura", e);
            }
        }
    }
}
