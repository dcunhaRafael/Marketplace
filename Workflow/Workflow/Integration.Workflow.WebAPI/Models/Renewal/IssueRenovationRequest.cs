namespace Integration.Workflow.WebAPI.Models.Renewal {
    public class IssueRenovationRequest {
        public int ProposalNumber { get; set; }
        public bool IsPropostaAssinada { get; set; }
        public int? IdTransacao { get; set; }
    }
}
