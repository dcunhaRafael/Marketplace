using Application.IoC.Services;
using Domain.Payload;
using Domain.Util.HttpClients;
using Domain.Util.Mail;
using Infrastructure.IoC.Repository;
using Integration.IoC.Legacy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using System.IO;
using System.Reflection;

namespace Infratructure.WindowsService.SelicSync {
    public class Program {
        //public IConfiguration Configuration { get; }

        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            //Host.CreateDefaultBuilder(args)
            //    .ConfigureServices((hostContext, services) => {
            //        services.AddHostedService<Worker>();
            //    });

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    config.AddJsonFile("appsettings.json", optional: false);
                })
                //.ConfigureLogging(options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
                .ConfigureServices((hostContext, services) => {

                    services.IntegrationLegacyIoC();
                    services.InfrastructureRepositoryIoC();
                    services.ApplicationServicesIoC();

                    IConfiguration configuration = hostContext.Configuration;
                    services.Configure<EmailSettings>(configuration.GetSection("EmailService"));
                    services.Configure<EndpointSettings>(configuration.GetSection("Endpoints"));
                    services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

                    services.AddHostedService<Worker>()
                        .Configure<EventLogSettings>(config => {
                            config.LogName = "RenovationEmailSender Service";
                            config.SourceName = "RenovationEmailSender Service Source";
                        });
                }).UseWindowsService();
        }

        private static string GetBasePath() {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var isDevelopment = environment == Environments.Development;
            //if (isDevelopment) {
            //    return Directory.GetCurrentDirectory();
            //}
            //using var processModule = Process.GetCurrentProcess().MainModule;
            //return Path.GetDirectoryName(processModule?.FileName);
            return @"C:\Temp\RenovationEmailSender";
        }
    }

}
