using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IProposalOccurrenceRepository {
        Task<IList<ProposalOccurrence>> ListAsync(Domain.Payload.ProposalOccurrenceFilters filters);
        Task<ProposalOccurrence> GetAsync(long proposalOccurrenceId);
        Task<long> AddAsync(ProposalOccurrence item);
        Task UpdateAsync(ProposalOccurrence item);
        Task UpdateStatusAsync(ProposalOccurrence item);

        Task<IList<User>> ListLiberationusersAsync(long proposalOccurrenceId);
    }
}
