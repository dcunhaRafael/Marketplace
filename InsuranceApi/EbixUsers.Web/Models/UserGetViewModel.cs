using System.ComponentModel.DataAnnotations;

namespace EbixUsers.Web.ViewModels {
    public class UserGetViewModel {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
