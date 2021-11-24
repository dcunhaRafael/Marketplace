namespace Domain.Entities {
    public class Broker {
        public int? BrokerId { get; set; }
        public string Name { get; set; }
        public long CpfCnpjNumber { get; set; }
        public string SusepCode { get; set; }
        public int LegacyUserId { get; set; }
        public string LegacyCode { get; set; }
    }
}
