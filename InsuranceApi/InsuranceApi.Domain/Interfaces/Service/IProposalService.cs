using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IProposalService {
        Task<ProposalReturnGravarEntity> AddAsync(ProposalEntity proposal, int brokerUserId, string brokerSusepCode);
        Task<ProposalEntity> GetAsync(int proposalCode, int brokerUserId, string brokerSusepCode);
        Task<PropostaRetornoVerificarAprovacaoEntity> CheckApprovalAsync(int proposalCode, int brokerUserId, string brokerSusepCode);
        Task<ProposalReturnAprovarEntity> ApproveAsync(int proposalCode, int brokerUserId, string brokerSusepCode);
        Task<PropostaRetornoImprimirEntity> PrintAsync(int endorsementId);
        Task<ProposalEntity> GetAsync(long policyNumber, int brokerUserId, string brokerSusepCode);
    }
}
