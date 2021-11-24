using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class OccurrenceTypeLiberationUserRepository : BaseRepository, IOccurrenceTypeLiberationUserRepository {

        public OccurrenceTypeLiberationUserRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<OccurrenceTypeLiberationUser>> ListAsync(int occurrenceTypeId) {
            var query = new StringBuilder()
                .AppendLine("SELECT olu.*, u.Id as UserId, RTRIM(LTRIM(u.Name)) as Name, u.Email")
                .AppendLine("FROM OccurrenceTypeLiberationUser olu")
                .AppendLine("INNER JOIN Users u ON U.Id = olu.UserId")
                .AppendLine("WHERE olu.OccurrenceTypeId = @OccurrenceTypeId")
                .AppendLine("AND olu.Status = @Active")
                .AppendLine("ORDER BY u.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<OccurrenceTypeLiberationUser, User, OccurrenceTypeLiberationUser>(
                query.ToString(),
                param: new { OccurrenceTypeId = occurrenceTypeId, Active = (int)RecordStatusEnum.Active },
                map: (olu, u) => {
                    olu.User = u;
                    return olu;
                },
                splitOn: "UserId");

            return entity.ToList();
        }

        public async Task AddAsync(OccurrenceTypeLiberationUser item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO OccurrenceTypeLiberationUser ( OccurrenceTypeId, UserId, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @OccurrenceTypeId, @UserId, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(sbSql.ToString(),
                                                      new {
                                                          item.OccurrenceTypeId,
                                                          item.UserId,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });
        }

        public async Task UpdateAsync(OccurrenceTypeLiberationUser item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE OccurrenceTypeLiberationUser")
                .AppendLine("SET OccurrenceTypeId = @OccurrenceTypeId, ")
                .AppendLine("   UserId = @UserId, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE OccurrenceTypeLiberationUserId = @OccurrenceTypeLiberationUserId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.OccurrenceTypeLiberationUserId,
                    item.OccurrenceTypeId,
                    item.UserId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusNotInAsync(OccurrenceType master, IList<int> ids) {
            var query = new StringBuilder()
                .AppendLine("UPDATE OccurrenceTypeLiberationUser")
                .AppendLine("SET Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE OccurrenceTypeId = @OccurrenceTypeId AND UserId NOT IN @NotInIds AND Status = @Active");

            using var dbConn = GetMarketplaceDbConnection();
            await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    NotInIds = ids.ToArray(),
                    master.OccurrenceTypeId,
                    master.Status,
                    master.LastChangeUserId,
                    master.LastChangeDate,
                    Active = (int)RecordStatusEnum.Active
                });
        }
    }
}
