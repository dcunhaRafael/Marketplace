using FluentValidation;
using EbixUsers.Web.ViewModels;

namespace EbixUsers.Web.Models.Validation {
    public class UserAddValidator : AbstractValidator<UserAddViewModel> {

        public UserAddValidator() {
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
               .WithMessage("Obrigatório informar o e-mail do usuário.")
               .EmailAddress()
               .WithMessage("Email inválido");

            RuleFor(m => m.BrokerUserId)
               .NotEmpty()
               .WithMessage("Obrigatório informar o código de usuário do corretor.")
               .NotNull()
               .WithMessage("Obrigatório informar o código de usuário do corretor.");
        }
    }
}
