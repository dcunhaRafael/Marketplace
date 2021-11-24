using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class DocumentValidator : AbstractValidator<DocumentViewModel> {

        public DocumentValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.NomeArquivo)
           .NotEmpty().WithMessage("Obrigatório informar o nome do arquivo.")
           .NotNull().WithMessage("Obrigatório informar o nome do arquivo.");

           RuleFor(m => m.ConteudoBase64)
          .NotEmpty().WithMessage("Obrigatório informar contéudo base 64.")
          .NotNull().WithMessage("Obrigatório informar contéudo base 64.");
        }
    }
}
