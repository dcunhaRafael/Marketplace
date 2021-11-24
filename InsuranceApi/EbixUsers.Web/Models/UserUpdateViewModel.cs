using System.ComponentModel.DataAnnotations;

namespace EbixUsers.Web.ViewModels {
    public class UserUpdateViewModel {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int BrokerUserId { get; set; }
    }
}
