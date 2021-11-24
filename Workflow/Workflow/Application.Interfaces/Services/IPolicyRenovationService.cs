using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IPolicyRenovationService {
        Task<IList<PolicyRenovation>> ListAsync(int policyBatchId);
        Task<PolicyRenovation> GetAsync(int policyRenovationId);
    }
}
