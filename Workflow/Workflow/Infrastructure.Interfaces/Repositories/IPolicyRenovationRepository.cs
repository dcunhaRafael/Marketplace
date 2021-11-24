using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPolicyRenovationRepository {
        Task<IList<PolicyRenovation>> ListAsync(PolicyRenovation filters);
        Task<PolicyRenovation> GetAsync(int policyRenovationId);
        Task<int> AddAsync(PolicyRenovation item);
        Task UpdateAsync(PolicyRenovation item);

        Task<IList<PolicyRenovation>> ListProposalCreationPendingAsync();
    }
}
