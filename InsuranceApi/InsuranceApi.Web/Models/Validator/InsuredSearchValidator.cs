using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class InsuredSearchValidator : AbstractValidator<InsuredSearchViewModel> {

        public InsuredSearchValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.CpfCnpj)
           .NotNull().WithMessage("Obrigatório informar o cpf ou cnpj do segurado.")
           .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o segurado.");
        }

        private static bool ValidaCpfCnpj(string cpfCnpj) {
            return CpfCnpjUtils.IsValid(cpfCnpj); ;
        }
    }
}

