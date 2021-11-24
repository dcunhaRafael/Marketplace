using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Infra.CrossCutting.Identity.Context {
    public class IdentityAppDbContext : IdentityDbContext<ApplicationUser> {    
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
               .Property(e => e.ExternalId)
               .HasColumnType("int");
        }
    }
}
