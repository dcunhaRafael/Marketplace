using Domain.Enumerators;

namespace Domain.Entities {
    public class ComissionStatementStatus : BaseEntity {

        public ComissionStatementStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }

    }
}
