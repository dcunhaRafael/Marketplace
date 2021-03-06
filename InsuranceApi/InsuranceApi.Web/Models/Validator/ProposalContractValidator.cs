using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.Models.Validation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validator {
    public class ProposalContractValidator : AbstractValidator<ProposalContractViewModel> {

        public ProposalContractValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {


            RuleFor(m => m.CodigoProduto)
           .NotEmpty()
           .NotNull()
           .WithMessage("Obrigatório informar o codigo do produto.");

            RuleFor(m => m.CodigoModalidade)
           .NotEmpty()
           .NotNull()
           .WithMessage("Obrigatório informar a modalidade.");

            RuleFor(m => m.NumeroContrato)
           .NotEmpty()
           .WithMessage("Obrigatório informar o número da contrato.");

            RuleFor(m => m.DiasPrazoVigencia)
           .NotEmpty()
           .WithMessage("Obrigatório informar o dias do prazo da vigência.");

            RuleFor(m => m.ValorGarantia)
           .NotNull()
           .WithMessage("Obrigatório informar o valor da garantia.");

            RuleFor(m => m.CpfCnpjTomador)
           .NotEmpty()
           .WithMessage("Obrigatório informar o cpf ou cnpj do tomador.")
           .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o tomador.");

            RuleFor(customer => customer.Segurado).SetValidator(new InsuredValidator());

        }
        private static bool ValidaCpfCnpj(string cpfCnpj) {
            return CpfCnpjUtils.IsValid(cpfCnpj); ;
        }
    }
}

