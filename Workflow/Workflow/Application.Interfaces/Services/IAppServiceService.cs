using Domain.Payload;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Interfaces.Services {
    public interface IAppServiceService {
        Task<IList<AppService>> ListAsync();
        Task<AppService> GetAsync(int appServiceId);
        Task<AppService> GetAsync(string name);
        Task<int> SaveAsync(AppService item);
        Task DeleteAsync(AppService item);

        Task SendKeepAliveAsync(int appServiceId);
        Task UpdateExecutionAsync(int appServiceId, ExecutionStatusEnum status, string message, string data);
        Task AddLogAsync(int appServiceId, LogLevel logLevel, string message);
    }
}
