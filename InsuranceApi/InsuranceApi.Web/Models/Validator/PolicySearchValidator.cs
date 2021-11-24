using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class PolicySearchValidator : AbstractValidator<PolicySearchViewModel> {

        public PolicySearchValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {

            When(m => m.DataFinal != null, () =>
            {
               RuleFor(m => m.DataInicial)
               .NotEmpty().WithMessage("Obrigatório informar a data inicial")
               .NotNull().WithMessage("Obrigatório informar a data inicial");
            });

            When(m => m.DataInicial != null, () =>
            {
               RuleFor(m => m.DataFinal)
               .NotEmpty().WithMessage("Obrigatório informar a data final")
               .NotNull().WithMessage("Obrigatório informar a data final");
            });
        }
    }
}