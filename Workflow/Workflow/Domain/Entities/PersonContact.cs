namespace Domain.Entities {
    public class PersonContact : BaseEntity {
        public PersonContact() {
            ContactType = new FixedDomain();
        }

        public int Id { get; set; }
        public int PersonId { get; set; }
        public FixedDomain ContactType { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }

    }
}
