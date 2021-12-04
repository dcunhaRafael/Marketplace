namespace Domain.Payload {
    public class ComissionStatementPayment {
        public string Name { get; set; }
        public decimal DebitValue { get; set; }
        public decimal CreditValue { get; set; }
    }
}
