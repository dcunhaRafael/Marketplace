namespace Domain.Entities {
    public class PaymentInstallment : BaseEntity {

        public PaymentInstallment() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? InstallmentCount { get; set; }
        public string LegacyCode { get; set; }
    }
}
