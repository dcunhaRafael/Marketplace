
namespace Domain.Payload {
    public class ComissionStatementEntry {
        public ComissionStatementEntry() {
        }

        public int StatementNumber { get; set; }
        public string Competency { get; set; }
        public int EntryNumber { get; set; }
        public int ProposalNumber { get; set; }
        public long PolicyNumber { get; set; }
        public int EndorsementNumber { get; set; }
        public int ComissionTypeId { get; set; }
        public string ComissionTypeName { get; set; }
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public int InstallmentNumber { get; set; }
        public string InsuredName { get; set; }
        public decimal? ComissionPercentage { get; set; }
        public decimal? TariffPremiumValue { get; set; }
        public decimal? ComissionValue { get; set; }

    }
}
