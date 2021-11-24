namespace Domain.Payload {
    public class ProposalOccurrenceForward {
        public int ProposalNumber { get; set; }
        public long? ProposalOccurrenceId { get; set; }
        public string UserComments { get; set; }
        public int ForwardUserId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
