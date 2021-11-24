
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class AppServiceRepository : BaseRepository, IAppServiceRepository {

        public AppServiceRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<AppService>> ListAsync() {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM AppService")
                .AppendLine("ORDER BY status, Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<AppService>(query.ToString());

            return entity.ToList();
        }

        public async Task<AppService> GetAsync(int appServiceId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM AppService")
                .AppendLine("WHERE AppServiceId = @AppServiceId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<AppService>(
                query.ToString(),
                param: new {
                    AppServiceId = appServiceId
                });

            return entity.FirstOrDefault();
        }

        public async Task<AppService> GetAsync(string name) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM AppService")
                .AppendLine("WHERE Name = @Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<AppService>(
                query.ToString(),
                param: new {
                    Name = name
                });

            return entity.FirstOrDefault();
        }

        public async Task<int> AddAsync(AppService item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO AppService ( Name, Timeout, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @Name, @Timeout, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.Name,
                                                          item.Timeout,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(AppService item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE AppService                                                                                 ")
                .AppendLine("SET Name = @Name, ")
                .AppendLine("   Timeout = @Timeout, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE AppServiceId = @AppServiceId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.AppServiceId,
                    item.Name,
                    item.Timeout,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusAsync(AppService item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE AppService                                                                                 ")
                .AppendLine("SET Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE AppServiceId = @AppServiceId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.AppServiceId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateKeepAliveAsync(int appServiceId) {
            var query = new StringBuilder()
                .AppendLine("UPDATE AppService                                                                                 ")
                .AppendLine("SET KeepAlive = @KeepAlive")
                .AppendLine("WHERE AppServiceId = @AppServiceId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    KeepAlive = DateTime.Now,
                    AppServiceId = appServiceId,
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateExecutionAsync(AppService item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE AppService                                                                                 ")
                .AppendLine("SET ExecutionStatus = @ExecutionStatus,")
                .AppendLine("   ExecutionDate = @ExecutionDate,")
                .AppendLine("   ExecutionMessage = @ExecutionMessage,")
                .AppendLine("   ExecutionData = @ExecutionData")
                .AppendLine("WHERE AppServiceId = @AppServiceId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.ExecutionStatus,
                    ExecutionDate = DateTime.Now,
                    item.ExecutionMessage,
                    item.ExecutionData,
                    item.AppServiceId,
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }
    }
}
