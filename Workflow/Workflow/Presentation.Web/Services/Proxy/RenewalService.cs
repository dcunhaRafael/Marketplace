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
    public class RenewalService : BaseLogger, IRenewalService {
        private readonly IServiceFactory serviceFactory;
        private const string SERVICE_NAME = "renewal/";

        public RenewalService(ILogger<RenewalService> logger, IServiceFactory serviceFactory) : base(logger) {
            this.serviceFactory = serviceFactory;
        }

        public void Dispose() {
            GC.KeepAlive(serviceFactory);
        }

        public IList<PolicyBatch> ListBatches(PolicyBatch filters) {
            var serviceMethodName = "ListBatches";
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = filters;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<PolicyBatch>>(serviceResponse.Data);
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

        public PolicyBatch GetBatch(int policyBatchId) {
            var serviceMethodName = "GetBatch";
            var methodParameters = new { policyBatchId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyBatchId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<PolicyBatch>(serviceResponse.Data);
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

        public IList<PolicyRenovation> ListBatchItems(int policyBatchId) {
            var serviceMethodName = "ListBatchItems";
            var methodParameters = new { policyBatchId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyBatchId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<PolicyRenovation>>(serviceResponse.Data);
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

        public PolicyRenovation GetPolicy(int policyRenovationId) {
            var serviceMethodName = "GetPolicy";
            var methodParameters = new { policyRenovationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyRenovationId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<PolicyRenovation>(serviceResponse.Data);
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
