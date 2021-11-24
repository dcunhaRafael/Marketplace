using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IProposalOccurrenceService {
        Task<IList<ProposalOccurrence>> ListAsync(ProposalOccurrenceFilters filters);
        Task<ProposalOccurrence> GetAsync(long proposalOccurrenceId);
        Task<AnalyzeProposal> AnalyzeProposal(int proposalNumber, int loggedUserId);
        Task ApproveAsync(ProposalOccurrenceApprove item);
        Task RefuseAsync(ProposalOccurrenceRefuse item);
        Task ForwardAsync(ProposalOccurrenceForward item);
        Task<IList<ProposalOccurrenceDocument>> ListDocumentsAsync(long proposalOccurrenceId);
        Task<long> AddDocumentAsync(ProposalOccurrenceDocument item, int loggedUserId);
        Task<ProposalOccurrenceDocument> GetDocumentAsync(long proposalOccurrenceDocumentId);
        Task DeleteDocumentAsync(long proposalOccurrenceDocumentId, int loggedUserId);
        Task<IList<ProposalOccurrenceHistory>> ListHistoriesAsync(long proposalOccurrenceId);
        Task<IList<User>> ListLiberationUsersAsync(long proposalOccurrenceId);
    }
}
