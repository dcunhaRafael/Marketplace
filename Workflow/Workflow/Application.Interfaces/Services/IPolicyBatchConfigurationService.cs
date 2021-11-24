using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IPolicyBatchConfigurationService {
        Task<IList<PolicyBatchConfiguration>> ListAsync(PolicyBatchConfigurationFilters filters);
        Task<PolicyBatchConfiguration> GetAsync(int policyBatchConfigurationId);
        Task<int> SaveAsync(PolicyBatchConfiguration item);
        Task DeleteAsync(PolicyBatchConfiguration item);
        Task<IList<PolicyBatchConfigurationMail>> ListMailsAsync(int policyBatchConfigurationId);
        Task<PolicyBatchConfigurationMail> GetMailAsync(int policyBatchConfigurationMailId);
        Task<int> SaveMailAsync(PolicyBatchConfigurationMail item);
        Task DeleteMailAsync(PolicyBatchConfigurationMail item);
    }
}
