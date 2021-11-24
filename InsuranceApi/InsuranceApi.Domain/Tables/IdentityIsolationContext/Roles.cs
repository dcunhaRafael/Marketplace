using System;

namespace InsuranceApi.Domain.Tables.IdentityIsolationContext {
    public class Roles {
        public Roles() {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
