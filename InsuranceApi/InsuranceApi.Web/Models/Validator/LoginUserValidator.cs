using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class LoginUserValidator : AbstractValidator<LoginUsuarioViewModel> {

        public LoginUserValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.Login)
            .NotEmpty()
            .WithMessage("Obrigatório informar o login do usuário.");

            RuleFor(m => m.Password)
             .NotEmpty()
             .WithMessage("Obrigatório informar a senha do usuário.")
             .MinimumLength(6)
             .WithMessage("Password  precisa ter no minimo 6 caracteres.");
        }
    }
}

