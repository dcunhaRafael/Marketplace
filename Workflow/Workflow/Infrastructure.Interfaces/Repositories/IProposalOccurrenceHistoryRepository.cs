using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IProposalOccurrenceHistoryRepository {
        Task<IList<ProposalOccurrenceHistory>> ListAsync(long proposalOccurrenceId);
        Task AddAsync(ProposalOccurrenceHistory item);
    }
}
