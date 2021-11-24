namespace Integration.Workflow.WebAPI.Models.Proposal {
    public class DeleteOccurrenceDocumentRequest {
        public long ProposalOccurrenceDocumentId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
