namespace Domain.Entities {
    public class InstallmentStatus : BaseEntity {

        public InstallmentStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
