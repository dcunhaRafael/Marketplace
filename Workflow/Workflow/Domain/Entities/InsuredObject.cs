namespace Domain.Entities {
    public class InsuredObject : BaseEntity {

        public InsuredObject() {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string LegacyCode { get; set; }

    }
}
