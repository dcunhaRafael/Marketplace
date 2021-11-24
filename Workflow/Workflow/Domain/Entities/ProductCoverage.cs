namespace Domain.Entities {
    public class ProductCoverage : BaseEntity {

        public ProductCoverage() {
            Product = new Product();
            Coverage = new Coverage();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CoverageId { get; set; }

        public Product Product { get; set; }
        public Coverage Coverage { get; set; }

    }
}
