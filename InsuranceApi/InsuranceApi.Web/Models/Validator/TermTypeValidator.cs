using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class TermTypeValidator : AbstractValidator<TermTypeViewModel> {

        public TermTypeValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {

            RuleFor(m => m.CodigoModalidade)
           .NotEmpty()
           .WithMessage("Obrigatório informar a modalidade.")
           .NotNull()
           .WithMessage("Obrigatório informar a modalidade.");
        }
    }
}
