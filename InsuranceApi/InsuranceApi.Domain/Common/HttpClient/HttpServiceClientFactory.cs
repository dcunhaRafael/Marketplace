using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using InsuranceApi.Domain.BusinessObjects.AppSettings;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using System;

namespace InsuranceApi.Domain.Common.HttpClient {
    public class HttpServiceClientFactory : AppConfigHttp, IHttpServiceClientFactory {

        private readonly IFlurlClientFactory _flurlClientFactory;
        public HttpServiceClientFactory(
           IFlurlClientFactory flurlClientFactory,
           IOptions<ServiceSettings> serviceSettings) : base(serviceSettings) {


            _flurlClientFactory = flurlClientFactory;

        }
        public IFlurlClient GetService(ServiceClientTypeEnum serviceType) {
            switch (serviceType) {

                case ServiceClientTypeEnum.Proposal: {
                    return _flurlClientFactory.Get(AppConfigHttp.ProposalClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Broker: {
                    return _flurlClientFactory.Get(AppConfigHttp.BrokerClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Taker: {
                    return _flurlClientFactory.Get(AppConfigHttp.TakerClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.SignatureDigital: {
                    return _flurlClientFactory.Get(AppConfigHttp.SignatureDigitalClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.ProposalTransmission: {
                    return _flurlClientFactory.Get(AppConfigHttp.ProposalTransmissionClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Product: {
                    return _flurlClientFactory.Get(AppConfigHttp.ProductClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Coverage: {
                    return _flurlClientFactory.Get(AppConfigHttp.CoverageClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Policy: {
                    return _flurlClientFactory.Get(AppConfigHttp.PolicyClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.City: {
                    return _flurlClientFactory.Get(AppConfigHttp.CityClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Search: {
                    return _flurlClientFactory.Get(AppConfigHttp.SearchClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.Insured: {
                    return _flurlClientFactory.Get(AppConfigHttp.InsuredClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                case ServiceClientTypeEnum.ProposalSignatureDigital: {
                    return _flurlClientFactory.Get(AppConfigHttp.ProposalSignatureDigitalClientServiceEndpoint).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
                }
                default:
                    return _flurlClientFactory.Get("Teste");

            }
        }

        public IFlurlClient GetBCService(int seriesCode) {
            return _flurlClientFactory.Get(string.Format(AppConfigHttp.BCDataClientServiceEndpoint, seriesCode)).Configure(s => s.Timeout = DateTime.UtcNow.AddSeconds(10000).TimeOfDay);
        }
    }
}
