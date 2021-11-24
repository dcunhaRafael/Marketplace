namespace Domain.Entities {
    public class ProposalLink : BaseEntity {

        public ProposalLink() {
            ProductCoverage = new ProductCoverage();
        }

        public int Id { get; set; }
        public int ProductCoverageId { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public string ChartLegendName { get; set; }
        public string ChartBackgroundColor { get; set; }
        public string ChartBorderColor { get; set; }

        public ProductCoverage ProductCoverage { get; set; }
    }
}
