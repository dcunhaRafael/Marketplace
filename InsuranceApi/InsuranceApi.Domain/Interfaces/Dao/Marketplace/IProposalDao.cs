using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IProposalDao {
        Task<int> AddAsync(ProposalEntity item, int commercialStructureId, int brokerId, int takerId, int insuredId, int insuredTypeId, int proposalTypeId, int productId, int coverageId,
            int proposalStatusId, string proposalStatusName, int insuredObjectId, int? legalRecourseTypeParameterId, int? recursalPolicyExpDateId, int? bureauId, int? registryBureauId);
        Task<int> GetAsync(int proposalCode);
        Task UpdateStatusAsync(int proposalCode, ProposalStatusEntity statusCode);
        Task UpdatePolicyAsync(int proposalCode, long policyNumber, ProposalStatusEntity statusCode);
    }
}
