using FluentValidation;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class ProposalAppealValidator : AbstractValidator<ProposalAppealViewModel> {

        public ProposalAppealValidator() {
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

            RuleFor(m => m.CodigoPrazoAno)
           .NotNull()
           .WithMessage("Obrigatório informar o prazo.");

            RuleFor(m => m.NumeroProcesso)
           .NotEmpty()
           .WithMessage("Obrigatório informar o número do processo.");

            RuleFor(m => m.CodigoTribunal)
           .NotNull()
           .WithMessage("Obrigatório informar o código do tribunal.");

           // RuleFor(m => m.CodigoVara)
           //.NotNull()
           //.WithMessage("Obrigatório informar o código da vara.");

            RuleFor(m => m.CodigoTipoRecurso)
           .NotNull()
           .WithMessage("Obrigatório informar o tipo de recurso.");

            RuleFor(m => m.CpfCnpjTomador)
           .NotEmpty()
           .WithMessage("Obrigatório informar o cpf ou cnpj do tomador.")
           .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o tomador.");

            RuleFor(m => m.CpfCnpjReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o cpf ou cnpj do reclamente.")
           .Must(ValidaCpfCnpj).WithMessage("O cpf ou cnpj não é valido para o reclamente.");

            RuleFor(m => m.NomeRazaoSocialReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o nome da razão social do reclamente.");

            RuleFor(m => m.CepReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o cep do reclamente.");

            RuleFor(m => m.NumeroReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o  número do endereço.");

            RuleFor(m => m.EnderecoReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o endereço do reclamente.");

            RuleFor(m => m.UfReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar a uf do reclamente.");

            RuleFor(m => m.CidadeReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar a cidade do reclamente.");

            RuleFor(m => m.BairroReclamente)
           .NotEmpty()
           .WithMessage("Obrigatório informar o bairro  do reclamente.");
        }

        private static bool ValidaCpfCnpj(string cpfCnpj) {
            return CpfCnpjUtils.IsValid(cpfCnpj); ;
        }
    }
}
