using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IProposalOccurrenceDocumentRepository {
        Task<IList<ProposalOccurrenceDocument>> ListAsync(long proposalOccurrenceId);
        Task<ProposalOccurrenceDocument> GetAsync(long proposalOccurrenceDocumentId, bool includeFileContents);
        Task<long> AddAsync(ProposalOccurrenceDocument item);
        Task UpdateStatusAsync(ProposalOccurrenceDocument item);
    }
}
