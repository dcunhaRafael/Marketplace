using System.ComponentModel.DataAnnotations;

namespace EbixUsers.Web.ViewModels {
    public class UserUpdatePasswordViewModel {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
