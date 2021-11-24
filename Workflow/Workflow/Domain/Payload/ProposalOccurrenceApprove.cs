namespace Domain.Payload {
    public class ProposalOccurrenceApprove {
        public long? ProposalOccurrenceId { get; set; }
        public string UserComments { get; set; }
        public int LoggedUserId { get; set; }
    }
}
