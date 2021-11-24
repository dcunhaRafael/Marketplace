using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class ApproveProposalValidator : AbstractValidator<ApproveProposalViewModel> {

        public ApproveProposalValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }
        private void Validation() {
            RuleFor(m => m.CodigoProposta)
            .NotEmpty()
            .WithMessage("Obrigatório informar o número da proposta.")
            .NotNull()
            .WithMessage("Obrigatório informar o número do proposta.");
            
        }
    }
}
