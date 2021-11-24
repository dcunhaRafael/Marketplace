using Flurl.Http;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;

namespace InsuranceApi.Domain.Interfaces.Common {
    public interface IHttpServiceClientFactory {
        IFlurlClient GetService(ServiceClientTypeEnum serviceType);
        IFlurlClient GetBCService(int seriesCode);
    }
}
