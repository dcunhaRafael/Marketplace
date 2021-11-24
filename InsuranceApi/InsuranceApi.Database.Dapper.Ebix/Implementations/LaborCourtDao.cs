using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using InsuranceApi.Domain.Common.Exceptions;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class LaborCourtDao : BaseDao, ILaborCourtDao {
        private readonly IConfiguration configuration;

        public LaborCourtDao(IConfiguration configuration, IAppAuditorshipDao appAuditorshipDao) : base(appAuditorshipDao) {
            this.configuration = configuration;
        }

        public async Task<IList<LaborCourtEntity>> ListAsync(string name, RecordStatusEnum status) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT LaborCourtId, Name, Status, ExternalCode, ExternalPersonCode, ExternalAddressCode, UserId, DateUtc");
                sbSql.AppendLine("     , Address, AddressNumber, AddressComplement, District, City, State, ZipCode");
                sbSql.AppendLine("  FROM LaborCourt");
                sbSql.AppendLine("   WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')");
                sbSql.AppendLine("  AND Status = @Status ");
                var item = await connection.QueryAsync<LaborCourtEntity>(sbSql.ToString(), new {
                    Name = name,
                    Status = status
                });
                return item.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando vara", e);
            }
        }

        public async Task<LaborCourtEntity> GetAsync(int laborCourtId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT LaborCourtId, Name, Status, ExternalCode, ExternalPersonCode, ExternalAddressCode, UserId, DateUtc");
                sbSql.AppendLine("     , Address, AddressNumber, AddressComplement, District, City, State, ZipCode");
                sbSql.AppendLine("  FROM LaborCourt");
                sbSql.AppendLine(" WHERE LaborCourtId = @LaborCourtId");
                sbSql.AppendLine(" AND Status = 1");
                var item = await connection.QueryAsync<LaborCourtEntity>(sbSql.ToString(), new {
                    LaborCourtId = laborCourtId
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo vara", e);
            }
        }

        public async Task UpdateExternalCodeAsync(LaborCourtEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE LaborCourt");
                sbSql.AppendLine("   SET ExternalPersonCode = @ExternalPersonCode");
                sbSql.AppendLine("     , ExternalAddressCode = @ExternalAddressCode");
                sbSql.AppendLine(" WHERE LaborCourtId = @LaborCourtId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ExternalPersonCode = item.ExternalPersonCode,
                    ExternalAddressCode = item.ExternalAddressCode,
                    LaborCourtId = item.LaborCourtId,
                });
                WriteHistory(MethodBase.GetCurrentMethod(), item, item.LaborCourtId.Value, item.LoggedUserId);
            } catch (Exception e) {
                throw new DaoException("Erro atualizando vara", e);
            }
        }
    }
}
