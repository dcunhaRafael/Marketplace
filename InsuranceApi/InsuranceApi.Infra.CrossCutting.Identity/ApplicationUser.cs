using Microsoft.AspNetCore.Identity;

namespace InsuranceApi.Infra.CrossCutting.Identity {

    public class ApplicationUser : IdentityUser {
        public int ExternalId { get; set; }
    }
}