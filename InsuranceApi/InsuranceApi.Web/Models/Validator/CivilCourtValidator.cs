using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class CivilCourtValidator : AbstractValidator<CivilCourtViewModel> {

        public CivilCourtValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.CodigoTribunal)
            .NotEmpty().WithMessage("Obrigatório informar o codigo do tribunal.");
            RuleFor(m => m.CodigoTribunal)
            .NotNull().WithMessage("Obrigatório informar o codigo do tribunal.");
        }
    }
}
