namespace Domain.Payload {
    public class Taker {
        public int TakerId { get; set; }
        public string Name { get; set; }
        public long CpfCnpjNumber { get; set; }
        public string LegacyCode { get; set; }
    }
}