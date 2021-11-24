namespace Domain.Payload {
    public class City  {

        public City() {
            State = new State();
        }

        public int? Id { get; set; }
        public State State { get; set; }
        public string Name { get; set; }
    }
}
