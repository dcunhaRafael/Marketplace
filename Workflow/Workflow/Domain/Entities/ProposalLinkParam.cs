namespace Domain.Entities {
    public class ProposalLinkParam : BaseEntity {

        public ProposalLinkParam() {

        }

        public int Id { get; set; }
        public ProposalLink ProposalLink { get; set; }
        public string ProposalParams { get; set; }
        public string DataParams { get; set; }
        public string ProductionStructureParams { get; set; }
        public string TakerParams { get; set; }
        public string InsuredParams { get; set; }
        public string ClaimantParams { get; set; }
        public string InsuranceCoverParams { get; set; }
        public string ProcessInformationParams { get; set; }
        public string RequesterParams { get; set; }
        public string ClauseParams { get; set; }
        public string InsuredObjectParams { get; set; }
        public string BillingParams { get; set; }
        public string PremiumParams { get; set; }
        public string InstallmentParams { get; set; }

    }
}
