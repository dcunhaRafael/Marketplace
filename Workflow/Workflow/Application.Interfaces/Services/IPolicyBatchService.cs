using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IPolicyBatchService {
        Task<IList<PolicyBatch>> ListAsync(PolicyBatch filters);
        Task<IList<PolicyBatch>> ListNewAsync(PolicyBatchConfiguration filters);
        Task<PolicyBatch> GetAsync(int policyBatchId);
        Task<int> SaveAsync(PolicyBatch item, int processDays);
        Task DeleteAsync(PolicyBatch item);

        Task SendAlertMailAsync(PolicyBatchConfigurationMail config, PolicyBatchItem item);
        Task SendAlertMailFinishedAsync(int success, int errors);

        Task SendBatchSuccessMailAsync(IList<PolicyBatch> batches);
        Task SendBatchErrorMailAsync(IList<PolicyBatch> batches);

        Task<IList<PolicyBatchItem>> ListItemsAsync(int policyBatchId, int? expirationDays);

        Task<IList<PolicyRenovation>> ListProposalCreationPendingAsync();
        Task SavePolicyRenovationAsync(PolicyRenovation item);

    }
}
