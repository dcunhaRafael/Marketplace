using FluentValidation;
using EbixUsers.Web.ViewModels;

namespace EbixUsers.Web.Models.Validation {
    public class UserInactivateValidator : AbstractValidator<UserInactivateViewModel> {

        public UserInactivateValidator() {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Obrigatório informar o identificador do usuário.");
            //RuleFor(m => m.Login)
            //   .NotEmpty()
            //   .WithMessage("Obrigatório informar o login do usuário.");
        }
    }
}
