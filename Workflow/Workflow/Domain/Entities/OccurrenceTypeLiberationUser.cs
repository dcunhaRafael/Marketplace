namespace Domain.Entities {
    public class OccurrenceTypeLiberationUser : BaseEntity {
        public OccurrenceTypeLiberationUser() {
            this.User = new User();
        }

        public int? OccurrenceTypeLiberationUserId { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public int? UserId { get; set; }

        // Childs
        public User User { get; set; }
    }
}
