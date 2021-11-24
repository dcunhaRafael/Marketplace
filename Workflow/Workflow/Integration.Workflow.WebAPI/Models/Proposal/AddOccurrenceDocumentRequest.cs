namespace Integration.Workflow.WebAPI.Models.Proposal {
    public class AddOccurrenceDocumentRequest {
        public int DocumentTypeId { get; set; }
        public string FileName { get; set; }
        public string FileContentsBase64 { get; set; }
        public long ProposalOccurrenceId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
