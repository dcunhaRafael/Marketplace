using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class AppAuditorshipDao : IAppAuditorshipDao {
        private readonly IConfiguration configuration;

        public AppAuditorshipDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public void Add(AppAuditorshipEntity item) {
            using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("INSERT INTO AppAuditorship ( EntityClass, EntityId, ActionName, RecordData, UserId, DateUtc )");
            sbSql.AppendLine("VALUES ( @EntityClass, @EntityId, @ActionName, @RecordData, @UserId, @DateUtc ) ");
            connection.Execute(sbSql.ToString(),
                        new {
                            EntityClass = item.EntityClass,
                            EntityId = item.EntityId,
                            ActionName = item.ActionName,
                            RecordData = JsonConvert.SerializeObject(item.RecordData),
                            UserId = item.UserId,
                            DateUtc = item.DateUtc
                        });
        }
    }
}