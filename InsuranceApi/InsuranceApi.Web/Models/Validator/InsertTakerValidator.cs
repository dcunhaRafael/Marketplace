using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validator {
    public class InsertTakerValidator : AbstractValidator<InsertTakerViewModel> {

        public InsertTakerValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.CpfCnpj)
            .NotEmpty().WithMessage("Obrigatório informar o cpf ou cnpj do tomador.")
            .NotNull().WithMessage("Obrigatório informar o cpf ou cnpj do tomador.")
            .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o tomador.");

            RuleFor(taker => taker.Contact).SetValidator(new ContactValidator());
        }

        private static bool ValidaCpfCnpj(string cpfCnpj) {
            return CpfCnpjUtils.IsValid(cpfCnpj); ;
        }
    }
}

