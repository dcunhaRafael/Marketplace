using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Services.Proxy {
    public interface IProposalService {
        IList<ProposalOccurrence> ListOccurrences(ProposalOccurrenceFilters filters);
        ProposalOccurrence GetOccurrence(long proposalOccurrenceId);

        IList<User> ListOccurrenceLiberationUsers(long proposalOccurrenceId);
        IList<ProposalOccurrenceHistory> ListOccurrenceHistories(long proposalOccurrenceId);
        void ForwardOccurrence(ProposalOccurrenceForward item);
        IList<ProposalOccurrenceDocument> ListOccurrenceDocuments(long proposalOccurrenceId);
        long AddOccurrenceDocument(ProposalOccurrenceDocument item, int loggedUserId);
        ProposalOccurrenceDocument GetOccurrenceDocument(long proposalOccurrenceDocumentId);
        void DeleteOccurrenceDocument(long proposalOccurrenceDocumentId, int loggedUserId);
        void ApproveOccurrence(ProposalOccurrenceApprove item);
        void RefuseOccurrence(ProposalOccurrenceRefuse item);
    }
}
