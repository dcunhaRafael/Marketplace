using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Domain.Util.Extensions;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class FixedDomainRepository : BaseRepository, IFixedDomainRepository {

        public FixedDomainRepository(IDatabaseFactory databaseOptions): base(databaseOptions) { }

        public async Task<IList<FixedDomain>> ListAsync(FixedDomainGroupNameEnum group) {
            var query = new StringBuilder()
                .AppendLine("SELECT * FROM FixedDomain")
                .AppendLine("WHERE GroupName = @GroupName")
                .AppendLine("AND Status = @Active")
                .AppendLine("ORDER BY DisplayOrder");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entityList = await dbConn.QueryAsync<FixedDomain>(
                query.ToString(), 
                param: new { GroupName = group.GetDefaultValue(), Active = (int)RecordStatusEnum.Active });

            return entityList.ToList();
        }

        public async Task<IList<FixedDomain>> ListAsync(string name, FixedDomainGroupNameEnum? group) {
            var query = new StringBuilder()
                .AppendLine("SELECT * FROM FixedDomain")
                .AppendLine("WHERE (@GroupName IS NULL OR GroupName = @GroupName)")
                .AppendLine("AND (@Name IS NULL OR Name LIKE '%' + @Name + '%')")
                .AppendLine("AND Status = @Active")
                .AppendLine("ORDER BY DisplayOrder");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entityList = await dbConn.QueryAsync<FixedDomain>(
                query.ToString(),
                param: new { Name = name, GroupName = group?.GetDefaultValue(), Active = (int)RecordStatusEnum.Active });

            return entityList.ToList();
        }

        public async Task<FixedDomain> GetAsync(int id) {
            using var dbConn = base.GetMarketplaceDbConnection();
            var queryResult = await dbConn.QueryAsync<FixedDomain>(
                "SELECT * FROM FixedDomain WHERE Id = @Id",
                param: new { Id = id });

            return queryResult.FirstOrDefault();
        }

        public async Task<FixedDomain> GetAsync(FixedDomainGroupNameEnum group, string legacyCode) {
            using var dbConn = base.GetMarketplaceDbConnection(); 
            var queryResult = await dbConn.QueryAsync<FixedDomain>(
                "SELECT * FROM FixedDomain WHERE GroupName = @GroupName AND LegacyCode = @LegacyCode",
                param: new { GroupName = group.GetDefaultValue(), LegacyCode = legacyCode });

            return queryResult.FirstOrDefault();
        }

        public async Task<int?> GetIdAsync(FixedDomainGroupNameEnum group, string legacyCode) {
            var query = new StringBuilder()
                .AppendLine("SELECT Id FROM FixedDomain")
                .AppendLine("WHERE GroupName = @GroupName AND LegacyCode = @LegacyCode");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int?>(
                    query.ToString(),
                    param: new { GroupName = group.GetDefaultValue(), LegacyCode = legacyCode });

            return id.FirstOrDefault();
        }

        public async Task UpdateAsync(FixedDomain item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE Domain")
                .AppendLine("SET Name = @Name, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE Id = @Id");

            using var dbConn = GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.Id,
                    item.Name,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }
    }
}
