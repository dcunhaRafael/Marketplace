using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Portal.Web.Models.ComissionStatement {
    public class ComissionStatementViewModel {

        public ComissionStatementViewModel() {
            this.Broker = new Broker();
            this.Statements = new List<Domain.Payload.ComissionStatement>();
            this.Status = new List<ComissionStatementStatus>();
            this.Summary = new List<ComissionStatementSummary>();
            this.CurrentStatement = new Domain.Payload.ComissionStatement();
            this.StatementEntries = new List<ComissionStatementEntry>();
            this.StatementTypes = new List<ComissionStatementType>();
            this.StatementBusiness = new List<ComissionStatementBusiness>();
            this.StatementDetails = new List<ComissionStatementDetail>();
        }

        public Broker Broker { get; set; }
        public int? StatementNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StatusId { get; set; }
        public string Competency { get; set; }

        public IList<ComissionStatementStatus> Status { get; set; }

        public IList<ComissionStatementSummary> Summary { get; set; }
        public IList<Domain.Payload.ComissionStatement> Statements { get; set; }

        public Domain.Payload.ComissionStatement CurrentStatement { get; set; }
        public IList<ComissionStatementEntry> StatementEntries { get; set; }
        public IList<ComissionStatementType> StatementTypes { get; set; }
        public IList<ComissionStatementBusiness> StatementBusiness { get; set; }
        public IList<ComissionStatementDetail> StatementDetails { get; set; }
    }
}
