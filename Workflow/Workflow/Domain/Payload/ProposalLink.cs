namespace Domain.Payload {
    public class ProposalLink {
        public int? ProposalLinkId { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public Coverage Coverage { get; set; }
    }
}
