namespace Domain.Entities {
    public class AddressType : BaseEntity {

        public AddressType() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
    }
}
