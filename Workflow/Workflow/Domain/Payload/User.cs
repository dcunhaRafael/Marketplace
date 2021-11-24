namespace Domain.Payload {
    public class User {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Aux
        public bool IsChecked { get; set; }
        public int? ParentId { get; set; }
    }
}
