using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IPolicyService {
        Task<ApoliceRetornoEmitirEntity> IssuePolicyAsync(ApoliceAssinadaEmitirEntity policy, int brokerUserId);
        Task<BilletPrintEntity> GetBilletPrintAsync(int endorsementId);
        Task<PolicyPrintEntity> GetPolicyPrintAsync(int endorsementId);
        Task<List<PolicyReturnEntity>> ListPolicyAsync(PolicySearchEntity filters);

    }
}
