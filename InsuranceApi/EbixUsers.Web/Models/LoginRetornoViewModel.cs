using System.Collections.Generic;

namespace EbixUsers.Web.ViewModels {
    public class LoginUsuarioViewModel {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginRetornoViewModel {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class ClaimViewModel {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
