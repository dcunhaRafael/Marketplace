using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProposalApplication {
        Task<ProposalEntity> GetAsync(int proposalCode, int? proposalBrokerUserId);
        Task<ProposalReturnGravarEntity> AddAsync(ProposalEntity proposal);
        Task<PropostaRetornoVerificarAprovacaoEntity> CheckApproveProposalAsync(int proposalCode, int proposalBrokerUserId);
        Task<ProposalReturnEmitirEntity> ApproveAsync(int proposalCode, int proposalBrokerUserId);
        Task<PropostaRetornoImprimirEntity> PrintAsync(int endorsementId);
        Task<ProposalReturnGravarEntity> AddRenovalAsync(ProposalRenewalEntity proposal);
    }
}
