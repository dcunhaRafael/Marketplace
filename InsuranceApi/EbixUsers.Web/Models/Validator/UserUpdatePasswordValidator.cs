using FluentValidation;
using EbixUsers.Web.ViewModels;

namespace EbixUsers.Web.Models.Validation {
    public class UserUpdatePasswordValidator : AbstractValidator<UserUpdatePasswordViewModel> {

        public UserUpdatePasswordValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.Id)
               .NotEmpty()
               .WithMessage("Obrigatório informar o identificador do usuário.");

            RuleFor(m => m.OldPassword)
               .NotEmpty()
               .WithMessage("Obrigatório informar a senha atual do usuário.");

            RuleFor(m => m.NewPassword)
               .NotEmpty()
               .WithMessage("Obrigatório informar a nova senha do usuário.");
        }
    }
}
