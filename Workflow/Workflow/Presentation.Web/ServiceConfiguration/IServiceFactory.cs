using Domain.Util.HttpClients;

namespace Presentation.Web.ServiceConfiguration {
    public interface IServiceFactory {
        RestClient ServiceClient { get; }
    }
}
