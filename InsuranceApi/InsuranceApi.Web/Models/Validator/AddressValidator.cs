using FluentValidation;
using InsuranceApi.Web.ViewModels;

namespace InsuranceApi.Web.Models.Validation {
    public class AddressValidator : AbstractValidator<AddressViewModel> {

        public AddressValidator() {

            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            Validation();
        }

        private void Validation() {

            RuleFor(m => m.Cep)
            .NotEmpty()
            .WithMessage("Obrigatório informar o cep do segurado.");

            RuleFor(m => m.Numero)
           .NotEmpty()
           .WithMessage("Obrigatório informar o  número do endereço.");

            RuleFor(m => m.Endereco)
           .NotEmpty()
           .WithMessage("Obrigatório informar o endereço do segurado.");

            RuleFor(m => m.Uf)
           .NotEmpty()
           .WithMessage("Obrigatório informar a uf do segurado.");

            RuleFor(m => m.Cidade)
           .NotEmpty()
           .WithMessage("Obrigatório informar a cidade do segurado.");

            RuleFor(m => m.Bairro)
           .NotEmpty()
           .WithMessage("Obrigatório informar o bairro  do segurado.");
        }        
    }
}