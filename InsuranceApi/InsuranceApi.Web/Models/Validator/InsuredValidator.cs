using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class InsuredValidator : AbstractValidator<InsuredViewModel> {

        public InsuredValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.Nome)
           .NotEmpty().WithMessage("Obrigatório informar o nome do segurado.")
           .NotNull().WithMessage("Obrigatório informar o nome do segurado.");

            RuleFor(m => m.CpfCnpj)
            .NotEmpty()
            .WithMessage("Obrigatório informar o cpf ou cnpj do segurado.")
            .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o segurado.");

            RuleFor(customer => customer.Endereco).SetValidator(new AddressValidator());
        }

        private static bool ValidaCpfCnpj(string cpfCnpj) {
            return CpfCnpjUtils.IsValid(cpfCnpj); ;
        }
    }
}
