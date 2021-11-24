using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validator {
    public class ContactValidator : AbstractValidator<ContactViewModel> {

        public ContactValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }
        private void Validation() {

            When(m => !string.IsNullOrEmpty(m.Cpf), () =>
            {
                RuleFor(m => m.Nome)
               .NotEmpty().WithMessage("Obrigatório informar nome do contato.")
               .NotEmpty().WithMessage("Obrigatório informar nome do contato.");

                RuleFor(m => m.Cpf)
               .Must(ValidaCpfCnpj).WithMessage("O cpf não é valido para o  contato.");

                RuleFor(m => m.Email)
               .NotEmpty().WithMessage("Obrigatório informar email do contato.")
               .EmailAddress().WithMessage("Email do contato inválido");
            });
        }
        private static bool ValidaCpfCnpj(string cpf) {
            return CpfCnpjUtils.IsCpf(cpf); ;
        }
    }
}
