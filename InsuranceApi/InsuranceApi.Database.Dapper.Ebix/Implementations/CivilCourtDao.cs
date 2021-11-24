using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using InsuranceApi.Domain.Common.Exceptions;
using System.Collections.Generic;
using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class CivilCourtDao : BaseDao, ICivilCourtDao {
        private readonly IConfiguration configuration;

        public CivilCourtDao(IConfiguration configuration, IAppAuditorshipDao appAuditorshipDao) : base(appAuditorshipDao) {
            this.configuration = configuration;
        }

        public async Task<IList<CivilCourtEntity>> ListAsync(int laborCourtId, string name, RecordStatusEnum status) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT CivilCourtId, Name, Status, ExternalCode, ExternalPersonCode, ExternalAddressCode, UserId, DateUtc, LaborCourtId");
                sbSql.AppendLine("     , Address, AddressNumber, AddressComplement, District, City, State, ZipCode");
                sbSql.AppendLine("  FROM CivilCourt");
                sbSql.AppendLine(" WHERE LaborCourtId = @LaborCourtId");
                sbSql.AppendLine("  AND (@Name IS NULL OR Name LIKE '%' + @Name + '%')");
                sbSql.AppendLine("  AND Status = @Status ");
                var item = await connection.QueryAsync<CivilCourtEntity>(sbSql.ToString(), new {
                    LaborCourtId = laborCourtId,
                    Name = name,
                    Status = status
                });
                return item.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando varas", e);
            }
        }

        public async Task<CivilCourtEntity> GetAsync(int civilCourtId, int laborCourtId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT CivilCourtId, Name, Status, ExternalCode, ExternalPersonCode, ExternalAddressCode, UserId, DateUtc, LaborCourtId");
                sbSql.AppendLine("     , Address, AddressNumber, AddressComplement, District, City, State, ZipCode");
                sbSql.AppendLine("  FROM CivilCourt");
                sbSql.AppendLine(" WHERE CivilCourtId = @CivilCourtId");
                sbSql.AppendLine(" AND LaborCourtId = @LaborCourtId");
                sbSql.AppendLine(" AND Status = 1");
                var item = await connection.QueryAsync<CivilCourtEntity>(sbSql.ToString(), new {
                    CivilCourtId = civilCourtId,
                    LaborCourtId = laborCourtId
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro ao obter vara", e);
            }
        }

        public async Task UpdateExternalCodeAsync(CivilCourtEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE CivilCourt");
                sbSql.AppendLine("   SET ExternalPersonCode = @ExternalPersonCode");
                sbSql.AppendLine("     , ExternalAddressCode = @ExternalAddressCode");
                sbSql.AppendLine(" WHERE CivilCourtId = @CivilCourtId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ExternalPersonCode = item.ExternalPersonCode,
                    ExternalAddressCode = item.ExternalAddressCode,
                    CivilCourtId = item.CivilCourtId,
                });
                base.WriteHistory(MethodBase.GetCurrentMethod(), item, item.CivilCourtId.Value, item.LoggedUserId);
            } catch (Exception e) {
                throw new DaoException("Erro ao alterar vara", e);
            }
        }
    }
}
