using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IProposalRepository {
        Task<Proposal> GetAsync(int proposalNumber);
    }
}
