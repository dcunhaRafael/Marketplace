using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class UsuarioValidator : AbstractValidator<UserViewModel> {

        public UsuarioValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {

            RuleFor(m => m.Login)
           .NotEmpty()
           .WithMessage("Obrigatório informar o login do usuário.");

            RuleFor(m => m.Password)
           .NotEmpty()
           .WithMessage("Obrigatório informar a senha do usuário.");

            RuleFor(m => m.Email)
           .NotEmpty()
           .WithMessage("Obrigatório informar email do usuário.")
           .EmailAddress()
           .WithMessage("Email inválido");

            RuleFor(m => m.CodigoInternoUsuario)
           .NotEmpty()
           .WithMessage("Obrigatório informar o codigo interno do usuário.")
           .NotNull()
           .WithMessage("Obrigatório informar o codigo interno do usuário.");
        }
    }
}
