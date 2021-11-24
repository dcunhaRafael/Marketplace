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
    public class InsuredRepository : BaseRepository, IInsuredRepository {

        public InsuredRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<Insured>> ListAsync(string name, RecordStatusEnum? status) {
            var query = new StringBuilder()
                .AppendLine("SELECT InsuredId, NmPessoa as Name, CAST(NrCnpjCpf as bigint)  as CpfCnpjNumber, IdPessoa as LegacyCode")
                .AppendLine("FROM Insured")
                .AppendLine("WHERE NmPessoa LIKE '%' + @Name + '%'")
                .AppendLine("ORDER BY NmPessoa");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Insured>(
                query.ToString(),
                param: new { Name = name, Status = (int?)status }
            );

            return entity.ToList();
        }
    }
}
