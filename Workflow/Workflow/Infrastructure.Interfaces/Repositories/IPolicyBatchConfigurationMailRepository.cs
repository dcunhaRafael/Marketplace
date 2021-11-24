using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPolicyBatchConfigurationMailRepository {
        Task<IList<PolicyBatchConfigurationMail>> ListAsync(int policyBatchConfigurationId);
        Task<PolicyBatchConfigurationMail> GetAsync(int policyBatchConfigurationMailId);
        Task<int> AddAsync(PolicyBatchConfigurationMail item);
        Task UpdateAsync(PolicyBatchConfigurationMail item);
        Task UpdateStatusAsync(PolicyBatchConfigurationMail item);
    }
}
