using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IAppServiceRepository {
        Task<IList<AppService>> ListAsync();
        Task<AppService> GetAsync(int appServiceId);
        Task<AppService> GetAsync(string name);
        Task<int> AddAsync(AppService item);
        Task UpdateAsync(AppService item);
        Task UpdateStatusAsync(AppService item);
        Task UpdateKeepAliveAsync(int appServiceId);
        Task UpdateExecutionAsync(AppService item);
    }
}
