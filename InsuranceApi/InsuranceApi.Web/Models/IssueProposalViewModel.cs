using System.ComponentModel;
using System.Runtime.InteropServices;

namespace InsuranceApi.Web.ViewModels {
    public class IssueProposalViewModel {
        public int CodigoProposta { get; set; }

        [DefaultValue(false)]
        public bool IsPropostaAssinada { get; set; }
        public int? IdTransacao { get; set; }
    }
}
