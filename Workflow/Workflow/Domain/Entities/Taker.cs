namespace Domain.Entities {
    public class Taker {
        public Taker() {
            Person = new Person();
        }

        public int? TakerId { get; set; }
        public string Name { get; set; }            //TODO: Mudar
        public long CpfCnpjNumber { get; set; }     //TODO: Mudar
        public string LegacyCode { get; set; }
        public string AddressLegacyCode { get; set; }
        public Person Person { get; set; }
    }
}