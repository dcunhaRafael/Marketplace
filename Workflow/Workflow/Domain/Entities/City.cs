namespace Domain.Entities {
    public class City : BaseEntity {

        public City() {
            State = new State();
        }

        public int Id { get; set; }
        public State State { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }


        public bool IsChecked { get; set; }
    }
}
