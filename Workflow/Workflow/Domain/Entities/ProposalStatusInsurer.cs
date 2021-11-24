namespace Domain.Entities {
    public class ProposalStatusInsurer : BaseEntity {

        public ProposalStatusInsurer() {
            ProposalStatus = new ProposalStatus();
        }

        public int Id { get; set; }
        public int ProposalStatusId { get; set; }
        public int InsurerId { get; set; }
        public string LegacyCode { get; set; }

        public ProposalStatus ProposalStatus { get; set; }
    }
}
