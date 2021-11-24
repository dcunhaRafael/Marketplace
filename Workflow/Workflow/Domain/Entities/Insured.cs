namespace Domain.Entities {
    public class Insured {
        public Insured() {
            Person = new Person();
        }

        public int? InsuredId { get; set; }
        public string Name { get; set; }            //TODO: Mudar
        public long CpfCnpjNumber { get; set; }     //TODO: Mudar
        public string LegacyCode { get; set; }
        public string AddressLegacyCode { get; set; }
        public Person Person { get; set; }
    }
}

