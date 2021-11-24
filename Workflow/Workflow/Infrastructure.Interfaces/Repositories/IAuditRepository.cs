using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IAuditRepository {
        Task AddAsync(Audit item);
    }
}
