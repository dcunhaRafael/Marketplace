using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Infra.Data.Context
{
    public class DatabaseAppContext : DbContext
    {
        public DatabaseAppContext(DbContextOptions<DatabaseAppContext> options) : base(options) { }
    }
}
