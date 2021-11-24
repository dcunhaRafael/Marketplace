using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class PolicyValidator : AbstractValidator<IssueProposalViewModel> {

        public PolicyValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.CodigoProposta)
           .NotEmpty().WithMessage("Obrigatório informar o código da proposta.")
           .NotNull().WithMessage("Obrigatório informar o código da proposta.");

            When(m => m.IsPropostaAssinada, () =>
            {
                RuleFor(m => m.IdTransacao)
                .NotEmpty().WithMessage("Obrigatório informar o id da transação.")
                .NotNull().WithMessage("Obrigatório informar o id da transação.");
            });
        }
    }
}

