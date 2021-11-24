using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Domain.Util.Extensions;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class SelicMonthlyRepository : BaseRepository, ISelicMonthlyRepository {

        public SelicMonthlyRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<SelicMonthly>> ListAsync(DateTime fromDate, DateTime toDate) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM SelicMonthly")
                .AppendLine("WHERE Date >= @FromDate AND Date <= @ToDate")
                .AppendLine("ORDER BY Date");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<SelicMonthly>(
                query.ToString(),
                param: new { FromDate = fromDate, ToDate = toDate }
            );

            return entity.ToList();
        }

        public async Task SaveAsync(SelicMonthly item) {
            using var dbConn = base.GetMarketplaceDbConnection();
            var sbSql = new StringBuilder("UPDATE SelicMonthly SET Value = @Value WHERE Date = @Date");
            var recordAffected = await dbConn.ExecuteAsync(sbSql.ToString(), new { item.Date, item.Value });
            if (recordAffected == 0) {
                sbSql = new StringBuilder("INSERT INTO SelicMonthly ( Date, Value ) VALUES ( @Date, @Value )");
                await dbConn.ExecuteAsync(sbSql.ToString(), new { item.Date, item.Value });
            }
        }

        public async Task<IList<Domain.Payload.SelicCorrection>> ListCorrectionAsync(DateTime fromDate, DateTime toDate) {
            var query = new StringBuilder()
                .AppendLine("select taxa.[Date], taxa.Value as ValueFull, DiasMes.DiasUteis as WorkDays, DiasUtilizados.Dias as CorrectionDays, ")
                .AppendLine("	    taxa.value * (cast(DiasUtilizados.Dias as decimal(18,6)) / cast(DiasMes.DiasUteis as decimal(18,6))) as ValueCorrection")
                .AppendLine("from SelicMonthly as taxa")
                .AppendLine("inner join (")
                .AppendLine("	select year([date]) as Ano, MONTH([date]) as Mes, count(1) as DiasUteis")
                .AppendLine("	from SelicDaily")
                .AppendLine("	where [date] >= @FromBegin")
                .AppendLine("	and [date] <= @ToEnd")
                .AppendLine("	group by year([date]) , MONTH([date])")
                .AppendLine(") as DiasMes")
                .AppendLine("on DiasMes.Ano = YEAR(taxa.[Date]) and DiasMes.Mes = MONTH(taxa.[Date])")
                .AppendLine("inner join (")
                .AppendLine("	select year([date]) as Ano, MONTH([date]) as Mes, count(1) as Dias")
                .AppendLine("	from SelicDaily")
                .AppendLine("	where [date] >= @FromDate")
                .AppendLine("	and [date] <= @ToDate")
                .AppendLine("	group by year([date]) , MONTH([date])")
                .AppendLine(") as DiasUtilizados")
                .AppendLine("on DiasUtilizados.Ano = YEAR(taxa.[Date]) and DiasUtilizados.Mes = MONTH(taxa.[Date])")
                .AppendLine("ORDER BY taxa.[Date]");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<Domain.Payload.SelicCorrection>(
                query.ToString(),
                param: new { FromDate = fromDate.Date, ToDate = toDate.Date, FromBegin = fromDate.ToFirstDayOfMonth().Date, ToEnd = toDate.ToLastDayOfMonth().Date }
            );

            return entity.ToList();
        }
    }
}
