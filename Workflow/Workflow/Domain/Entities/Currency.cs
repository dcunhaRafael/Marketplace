namespace Domain.Entities {
    public class Currency : BaseEntity {

        public Currency() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string LegacyCode { get; set; }
    }
}
