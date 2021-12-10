using System.Collections.Generic;

namespace Domain.Payload {
    public class ComissionStatementCover {
        public ComissionStatementCover() {
            this.Statement = new ComissionStatement();
            this.Details = new ComissionStatementDetail();
            this.Types = new List<ComissionStatementType>();
            this.Business = new List<ComissionStatementBusiness>();
            this.Payments = new List<ComissionStatementPayment>();
        }

        public ComissionStatement Statement { get; set; }
        public ComissionStatementDetail Details { get; set; }
        public IList<ComissionStatementType> Types { get; set; }
        public IList<ComissionStatementBusiness> Business { get; set; }
        public IList<ComissionStatementPayment> Payments { get; set; }
    }
}
