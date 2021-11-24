namespace Domain.Payload {
    public class ProposalOccurrenceRefuse {
        public long? ProposalOccurrenceId { get; set; }
        public int? RefusalReasonId { get; set; }
        public string UserComments { get; set; }
        public int LoggedUserId { get; set; }
    }
}
