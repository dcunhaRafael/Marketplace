using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace RenewalApi.Web.Configuration {
    public static class ServiceSettingsConfig {

        public static IServiceCollection AddServiceSettingConfigureDictionary<TOptions>(this IServiceCollection services, IConfiguration configuration) where
        TOptions : class, IDictionary<string, string> {
            
            var appSettingsSection = configuration.GetSection("AppServiceSettings");            
            var values = appSettingsSection
                .GetChildren()
                .ToList();

            services.Configure<TOptions>(x =>
                values.ForEach(v => x.Add(v.Key, v.Value))
            );       

            return services;
        }
    }
}
