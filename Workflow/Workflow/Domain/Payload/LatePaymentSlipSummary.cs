namespace Domain.Payload {
    public class LatePaymentSlipSummary {
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public int? Count { get; set; }
        public decimal Percentage { get; set; }
        public string BackgroundColor { get; set; }
    }
}
