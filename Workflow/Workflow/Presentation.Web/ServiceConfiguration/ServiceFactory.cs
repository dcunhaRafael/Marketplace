using Domain.Util.HttpClients;
using Microsoft.Extensions.Options;

namespace Presentation.Web.ServiceConfiguration {
    public class ServiceFactory:  IServiceFactory {
        private IOptions<ServiceSettings> serviceSettings;
        
        protected string WorkflowWebAPI => !string.IsNullOrEmpty(serviceSettings.Value.WorkflowWebAPI) ?
                                                     serviceSettings.Value.WorkflowWebAPI :
                                                     ServiceConnection.ServiceConfiguration.GetSection("Endpoints")["WorkflowWebAPI"];

        public RestClient ServiceClient => new RestClient(WorkflowWebAPI);

        public ServiceFactory(IOptions<ServiceSettings> serviceSettings) {
            this.serviceSettings = serviceSettings;
        }
    }
}