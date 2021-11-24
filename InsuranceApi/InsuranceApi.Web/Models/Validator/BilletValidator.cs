using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class BilletValidator : AbstractValidator<BilletViewModel> {

        public BilletValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }
        private void Validation() {
            RuleFor(m => m.CodigoEndosso)
           .NotEmpty().WithMessage("Obrigatório informar o número do endosso.")
           .NotNull().WithMessage("Obrigatório informar o número do endosso.");
        }
    }
}
