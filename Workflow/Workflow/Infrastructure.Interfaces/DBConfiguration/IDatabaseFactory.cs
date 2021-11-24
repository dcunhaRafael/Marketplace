using System.Data;

namespace Infrastructure.Interfaces.DBConfiguration {
    public interface IDatabaseFactory {
        IDbConnection GetMarketplaceDbConnection { get; }
        IDbConnection GetStageDbConnection { get; }
    }
}
