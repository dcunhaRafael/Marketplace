using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class AuditRepository : BaseRepository, IAuditRepository {

        public AuditRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task AddAsync(Audit item) {
            StringBuilder query = new StringBuilder()
                .AppendLine("INSERT INTO Auditoria ( DataHora, TipoUsuarioId, UsuarioId, UrlChamada, Funcionalidade, TipoAcao, IP, Navegador, SistemaOperacional, Nivel, Request, Response )")
                .AppendLine("  VALUES ( @ActionDate, @UserTypeId, @UserId, @Url, @FeatureName, @ActionName, @IpAddress, @BrowserName, @OsName, @Level, @Request, @Response )");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.ExecuteAsync(
                query.ToString(),
                param: new {
                    item.Level,
                    item.ActionDate,
                    item.FeatureName,
                    item.ActionName,
                    item.Url,
                    item.IpAddress,
                    item.BrowserName,
                    item.OsName,
                    item.Request,
                    item.Response,
                    item.UserTypeId,
                    item.UserId
                });
        }
    }
}
