namespace Domain.Entities {
    public class PaymentFrequency : BaseEntity {

        public PaymentFrequency() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
