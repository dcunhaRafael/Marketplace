using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InsuranceApi.Web.Configuration {
    public static class AppsettingConfig {

        public static IConfigurationBuilder BuilderConfig() {

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())          
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

            return builder; ;
        }
    }
}
