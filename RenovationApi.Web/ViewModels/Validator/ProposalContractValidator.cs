using FluentValidation;
using RenewalApi.Web.ViewModels;

namespace RenewalApi.Web.Models.Validator {
    public class ProposalContractValidator : AbstractValidator<RenewalJudicialViewModel> {

        public ProposalContractValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {

            RuleFor(m => m.PolicyCode)
           .NotEmpty()
           .NotNull()
           .WithMessage("Obrigatório informar o número da apólice atual.");

           // RuleFor(m => m.BrokerDocument)
           //.NotEmpty()
           //.NotNull()
           //.WithMessage("Obrigatório informar o CNPJ do corretor.");

        }
    }
}

