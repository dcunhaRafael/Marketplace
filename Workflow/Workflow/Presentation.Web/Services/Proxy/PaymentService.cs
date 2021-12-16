using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.HttpClients;
using Domain.Util.Log;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Web.ServiceConfiguration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Presentation.Web.Services.Proxy {
    public class PaymentService : BaseLogger, IPaymentService {
        private readonly IServiceFactory serviceFactory;
        private const string SERVICE_NAME = "payment/";

        public PaymentService(ILogger<CommonService> logger, IServiceFactory serviceFactory) : base(logger) {
            this.serviceFactory = serviceFactory;
        }

        public void Dispose() {
            GC.KeepAlive(serviceFactory);
        }

        public IList<LatePaymentSlip> ListLatePaymentSlip(string brokerLegacyCode, string takerLegacyCode, string insuredLegacyCode, string productLegacyCode,
            long? policyNumber, int? endorsementNumber, int? installmentNumber, decimal? premiumValue, string ourNumber,
            DateTime? fromDate, DateTime? toDate) {
            var serviceMethodName = "ListLatePaymentSlip";
            var methodParameters = new {
                brokerLegacyCode,
                takerLegacyCode,
                insuredLegacyCode,
                productLegacyCode,
                policyNumber,
                endorsementNumber,
                installmentNumber,
                premiumValue,
                ourNumber,
                fromDate,
                toDate
            };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    BrokerLegacyCode = brokerLegacyCode,
                    TakerLegacyCode = takerLegacyCode,
                    InsuredLegacyCode = insuredLegacyCode,
                    ProductLegacyCode = productLegacyCode,
                    PolicyNumber = policyNumber,
                    EndorsementNumber = endorsementNumber,
                    InstallmentNumber = installmentNumber,
                    PremiumValue = premiumValue,
                    OurNumber = ourNumber,
                    FromDate = fromDate,
                    ToDate = toDate
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<LatePaymentSlip>>(serviceResponse.Data);
                return items;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public LatePaymentSlip GetLatePaymentSlip(string ourNumber) {
            var serviceMethodName = "GetLatePaymentSlip";
            var methodParameters = new { ourNumber };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    OurNumber = ourNumber
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<LatePaymentSlip>(serviceResponse.Data);
                return item;
            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }
    }
}
