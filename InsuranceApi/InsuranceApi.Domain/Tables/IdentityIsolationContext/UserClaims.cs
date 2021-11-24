namespace InsuranceApi.Domain.Tables.IdentityIsolationContext {
    public class UserClaims {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
