using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using InsuranceApi.Web.Configuration;
using Serilog;
using System;

namespace InsuranceApi.Web {
    public class Program {

        public static void Main(string[] args) {

            var configuration = AppsettingConfig.BuilderConfig().Build();

            Log.Logger = new LoggerConfiguration()                
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            try {             
                CreateHostBuilder(args).Build().Run();
            } catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
