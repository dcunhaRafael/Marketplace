using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class SearchProposaValidationcs : AbstractValidator<SearchProposalViewModel> {

        public SearchProposaValidationcs() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.CodigoProposta)
           .NotEmpty().WithMessage("Obrigatório informar o código da proposta.")
           .NotNull().WithMessage("Obrigatório informar o código da proposta.");
        }
    }
}
