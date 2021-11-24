using System.ComponentModel.DataAnnotations;

namespace InsuranceApi.Web.ViewModels {
    public class UserViewModel {

        public string Login { get; set; }
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        public int CodigoInternoUsuario { get; set; }
    }
}
