namespace Domain.Entities {
    public class PersonAddress : BaseEntity {

        public PersonAddress() {
            AddressType = new AddressType();
            City = new City();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool IsMainAddress { get; set; }
        public AddressType AddressType { get; set; }
        public int? ZipCode { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public City City { get; set; }
    }
}
