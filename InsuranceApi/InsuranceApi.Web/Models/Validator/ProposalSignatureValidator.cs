using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class ProposalSignatureValidator : AbstractValidator<ProposalSignatureViewModel> {

        public ProposalSignatureValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {                                 

        }
    }
}
