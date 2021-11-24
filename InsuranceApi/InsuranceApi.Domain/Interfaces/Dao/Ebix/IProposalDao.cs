using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IProposalDao {
        Task<int> GetAsync(int proposalCode);
        Task AddAsync(ProposalEntity item);
        Task UpdateStatusAsync(int proposalCode, int statusCode);
    }
}
