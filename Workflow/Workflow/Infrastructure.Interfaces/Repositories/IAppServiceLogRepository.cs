using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IAppServiceLogRepository {
        Task AddAsync(AppServiceLog item);
    }
}
