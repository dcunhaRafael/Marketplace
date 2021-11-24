using Integration.BMG;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.IoC.Legacy {
    public static class LegacyIoC {
        public static void IntegrationLegacyIoC(this IServiceCollection services) {
            services.AddScoped<IBacenService, BacenService>();
            services.AddScoped<ILegacyBrokerService, BMGBrokerService>();
            services.AddScoped<ILegacyTakerService, BMGTakerService>();
            services.AddScoped<IRenewalApiService, RenewalApiService>();
        }
    }
}
