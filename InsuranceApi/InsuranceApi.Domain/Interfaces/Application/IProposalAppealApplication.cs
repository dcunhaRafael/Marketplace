using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProposalAppealApplication {
        Task<ProposalReturnGravarEntity> AddAsync(ProposalAppealEntity entity, int brokerUserIdbrokerUserId);       
    }
}
