using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPolicyBatchConfigurationRepository {
        Task<IList<PolicyBatchConfiguration>> ListAsync(PolicyBatchConfiguration filters);
        Task<PolicyBatchConfiguration> GetAsync(int policyBatchConfigurationId);
        Task<int> AddAsync(PolicyBatchConfiguration item);
        Task UpdateAsync(PolicyBatchConfiguration item);
        Task UpdateStatusAsync(PolicyBatchConfiguration item);
    }
}
