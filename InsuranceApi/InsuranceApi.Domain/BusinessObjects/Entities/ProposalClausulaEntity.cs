namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalClausulaEntity {

        public long? ProposalClauseId { get; set; }
        public int? ProposalId { get; set; }
        public int? ClauseProductCoverageId { get; set; }
        public bool IsRequired { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? TextId { get; set; }
        public string Text { get; set; }

        // Auxiliares
        public bool IsChecked { get; set; }
    }
}
