using FluentValidation;
using InsuranceApi.Web.ViewModels;
namespace InsuranceApi.Web.Models.Validation {
    public class PolicyPrintValidator : AbstractValidator<PolicyPrintViewModel> {

        public PolicyPrintValidator() {
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
