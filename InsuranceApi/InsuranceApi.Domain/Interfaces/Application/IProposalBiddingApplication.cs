using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProposalBiddingApplication {
        Task<ProposalReturnGravarEntity> AddAsync(ProposalBiddingEntity entity, int proposalBrokerUserId);
    }
}
