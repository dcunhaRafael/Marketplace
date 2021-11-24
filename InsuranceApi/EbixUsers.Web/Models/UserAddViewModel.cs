using System.ComponentModel.DataAnnotations;

namespace EbixUsers.Web.ViewModels {
    public class UserAddViewModel {
        public string Login { get; set; }
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
        public int BrokerUserId { get; set; }
    }
}
