namespace Integration.Workflow.WebAPI.Models.ComissionStatement {
    public class ListComissionStatementBusinessRequest {
        public int StatementNumber { get; set; }
        public string Competency { get; set; }
        public string BrokerLegacyCode { get; set; }
        public string BrokerSusepCode { get; set; }
        public int BrokerUserId { get; set; }
        public int LoggedUserId { get; set; }
    }
}
