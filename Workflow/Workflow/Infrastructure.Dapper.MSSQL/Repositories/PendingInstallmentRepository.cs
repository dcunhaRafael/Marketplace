using Dapper;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class PendingInstallmentRepository : BaseRepository, IPendingInstallmentRepository {

        public PendingInstallmentRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<int> GetPendingCount(long takerCpfCnpjNumber) {
            var query = new StringBuilder()
                .AppendLine("SELECT COUNT(1)")
                .AppendLine("FROM ID0025_parcelas_pendentes")
                .AppendLine("WHERE nr_cnpj_cpf_tomador = @TakerCpfCnpjNumber");

            using var dbConn = base.GetStageDbConnection();
            var entity = await dbConn.QueryAsync<int>(
                query.ToString(),
                param: new { TakerCpfCnpjNumber = takerCpfCnpjNumber }
            );

            return entity.First();
        }
    }
}
