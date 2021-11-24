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
    public class TakerRepository : BaseRepository, ITakerRepository {

        public TakerRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<Taker>> ListAsync(int brokerId, string name, RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT Id as TakerId, NmPessoa as Name, CAST(NrCnpjCpf as bigint)  as CpfCnpjNumber, IdPessoa as LegacyCode")
                .AppendLine("FROM Taker")
                .AppendLine("WHERE BrokerId = @BrokerId")
                .AppendLine("AND NmPessoa LIKE '%' + @Name + '%'")
                .AppendLine("ORDER BY NmPessoa");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Taker>(
                query.ToString(),
                param: new { BrokerId = brokerId, Name = name, Status = (int?)status }
            );

            return entity.ToList();
        }
    }
}
