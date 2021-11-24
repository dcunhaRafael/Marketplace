using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Presentation.Web.ServiceConfiguration {
    public class ServiceConnection {
        public static IConfiguration ServiceConfiguration {
            get {
                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    //.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}
