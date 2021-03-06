using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProposalJudicialApplication {
        Task<ProposalReturnGravarEntity> AddAsync(ProposalJudicialEntity entity, int proposalBrokerUserId);
    }
}
