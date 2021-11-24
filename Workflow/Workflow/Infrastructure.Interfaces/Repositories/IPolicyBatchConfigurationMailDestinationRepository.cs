using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPolicyBatchConfigurationMailDestinationRepository {
        Task<IList<PolicyBatchConfigurationMailDestination>> ListAsync(int policyBatchConfigurationMailId);
        Task AddAsync(PolicyBatchConfigurationMailDestination item);
        Task UpdateAsync(PolicyBatchConfigurationMailDestination item);
        Task UpdateStatusNotInAsync(PolicyBatchConfigurationMail master, IList<int> ids);
    }
}
