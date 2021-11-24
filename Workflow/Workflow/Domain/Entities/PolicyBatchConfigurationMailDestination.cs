namespace Domain.Entities {
    public class PolicyBatchConfigurationMailDestination : BaseEntity {
        public PolicyBatchConfigurationMailDestination() {
            this.User = new User();
        }

        public int? PolicyBatchConfigurationMailDestinationId { get; set; }
        public int? PolicyBatchConfigurationMailId { get; set; }
        public int? UserId { get; set; }

        // Childs
        public User User { get; set; }
    }
}
