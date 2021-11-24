using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class BrokerRepository : BaseRepository, IBrokerRepository {

        public BrokerRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<Broker>> ListAsync(string name, RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT BrokerId, NmPessoa as Name, CAST(NrCnpjCpf as bigint) as CpfCnpjNumber, CdSusepCorretor as SusepCode, CAST(IdUsuarioCorretor as INT) as LegacyUserId, IdPessoa as LegacyCode")
                .AppendLine("FROM Broker")
                .AppendLine("WHERE NmPessoa LIKE '%' + @Name + '%'")
                .AppendLine("ORDER BY NmPessoa");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Broker>(
                query.ToString(),
                param: new { Name = name, Status = (int?)status }
            );

            return entity.ToList();
        }
    }
}
