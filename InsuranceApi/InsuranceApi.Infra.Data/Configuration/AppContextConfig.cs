using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceApi.Infra.Data.Context.Configuration {
    public static class AppContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseAppContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("PortalSeguros"));
            });            

            return services;
        }
    }
}
