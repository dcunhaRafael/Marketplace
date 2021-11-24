using FluentValidation;
using EbixUsers.Web.ViewModels;

namespace EbixUsers.Web.Models.Validation {
    public class UserUpdateValidator : AbstractValidator<UserUpdateViewModel> {

        public UserUpdateValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Obrigatório informar o identificador do usuário.");

            RuleFor(m => m.Login)
               .NotEmpty()
               .WithMessage("Obrigatório informar o login do usuário.");

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
