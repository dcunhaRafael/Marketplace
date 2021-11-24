namespace Domain.Entities {
    public class State : BaseEntity {

        public State() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string LegacyCode { get; set; }
    }
}
