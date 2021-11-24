using Domain.Payload;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IAuditService {
        Task AddAsync(Audit item);
    }
}
