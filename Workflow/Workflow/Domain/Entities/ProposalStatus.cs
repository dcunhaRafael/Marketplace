using Domain.Enumerators;

namespace Domain.Entities {
    public class ProposalStatus : BaseEntity {

        public ProposalStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }

    }
}
