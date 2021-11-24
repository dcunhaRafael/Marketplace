using System;

namespace Integration.Workflow.WebAPI.Models.ComissionStatement {
    public class ListComissionStatementRequest {
        public int? StatementNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Status { get; set; }
        public string BrokerLegacyCode { get; set; }
        public string BrokerSusepCode { get; set; }
        public int BrokerUserId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
