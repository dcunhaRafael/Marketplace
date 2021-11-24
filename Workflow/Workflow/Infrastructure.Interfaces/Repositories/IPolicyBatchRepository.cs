using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPolicyBatchRepository {
        Task<IList<PolicyBatch>> ListAsync(PolicyBatch filters);
        Task<IList<PolicyBatch>> ListNewAsync(PolicyBatchConfiguration filters);
        Task<PolicyBatch> GetAsync(int policyBatchId);
        Task<int> AddAsync(PolicyBatch item);
        Task UpdateAsync(PolicyBatch item);
        Task UpdateStatusAsync(PolicyBatch item);

        Task LinkBatchPoliciesAsync(PolicyBatch ite, int processDays);

        Task<IList<PolicyBatchItem>> ListItemsAsync(int policyBatchId, int? expirationDays);
    }
}
