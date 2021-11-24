using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProposalContractApplication {
        Task<ProposalReturnGravarEntity> AddAsync(ProposalContractEntity entity, int proposalBrokerUserId);
    }
}
