namespace Domain.Entities {
    public class ProposalCancel : BaseEntity {

        public ProposalCancel() {
            RefusalReason = new RefusalReason();
        }

        public int Id { get; set; }
        public int? ProposalId { get; set; }
        public RefusalReason RefusalReason { get; set; }
        public string Comments { get; set; }
    }
}
