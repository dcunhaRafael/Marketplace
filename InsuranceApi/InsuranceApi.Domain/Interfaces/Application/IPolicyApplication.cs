using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IPolicyApplication {
        Task<List<PolicyReturnEntity>> ListAsync(PolicySearchEntity filters);
        Task<ApoliceRetornoEmitirEntity> IssueAsync(IssueProposalEntity issueData, int? proposalBrokerUserId);
        Task<ApoliceRetornoEmitirEntity> IssueSignedAsync(int proposalCode, int transactionId, int? proposalBrokerUserId);
        Task<PolicyPrintEntity> PrintAsync(int endorsementCode);
        Task<BilletPrintEntity> PrintBilletAsync(int endorsementCode);
    }
}
