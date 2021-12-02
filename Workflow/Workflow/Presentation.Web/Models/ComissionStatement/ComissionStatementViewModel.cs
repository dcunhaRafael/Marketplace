using Domain.Enumerators;
using Domain.Payload;
using System;
using System.Collections.Generic;
using System.Globalization;

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

            this.SearchPeriodId = SearchRangeEnum.Last30Days;
            this.YearNumber = DateTime.Now.Year;
            this.MonthNumber = null;
        }

        public Broker Broker { get; set; }
        public int? StatementNumber { get; set; }
        public int? StatusId { get; set; }
        public SearchRangeEnum SearchPeriodId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public IList<ComissionStatementStatus> Status { get; set; }

        public int? YearNumber { get; set; }
        public Dictionary<int, int> Years {
            get {
                var months = new Dictionary<int, int>();
                for (int i = DateTime.Now.Year; i >= DateTime.Now.AddYears(-20).Year; i--) {
                    months.Add(i, i);
                }
                return months;
            }
        }

        public int? MonthNumber { get; set; }
        public Dictionary<int, string> Months {
            get {
                var months = new Dictionary<int, string>();
                for (int i = 1; i < 13; i++) {
                    months.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
                }
                return months;
            }
        }

        public string Competency { get; set; }

        public IList<ComissionStatementSummary> Summary { get; set; }
        public IList<Domain.Payload.ComissionStatement> Statements { get; set; }

        public Domain.Payload.ComissionStatement CurrentStatement { get; set; }
        public IList<ComissionStatementEntry> StatementEntries { get; set; }
        public IList<ComissionStatementType> StatementTypes { get; set; }
        public IList<ComissionStatementBusiness> StatementBusiness { get; set; }
        public IList<ComissionStatementDetail> StatementDetails { get; set; }
    }
}
