using System.ComponentModel;

namespace RenewalApi.Web.ViewModels {
    public class PolicyIssueViewModel {
        public int NumeroProposta { get; set; }
        [DefaultValue(false)]
        public bool IsPropostaAssinada { get; set; }
        public int? IdTransacao { get; set; }
    }
}
