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
    public class ConfigurationService : BaseLogger, IConfigurationService {
        private readonly IServiceFactory serviceFactory;
        private const string SERVICE_NAME = "configuration/";

        public ConfigurationService(ILogger<ConfigurationService> logger, IServiceFactory serviceFactory) : base(logger) {
            this.serviceFactory = serviceFactory;
        }

        public void Dispose() {
            GC.KeepAlive(serviceFactory);
        }

        public IList<OccurrenceType> ListOccurrenceTypes(OccurrenceTypeFilters filters) {
            var serviceMethodName = "ListOccurrenceTypes";
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
                var items = JsonConvert.DeserializeObject<IList<OccurrenceType>>(serviceResponse.Data);
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

        public OccurrenceType GetOccurrenceType(int occurrenceTypeId) {
            var serviceMethodName = "GetOccurrenceType";
            var methodParameters = new { occurrenceTypeId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { occurrenceTypeId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<OccurrenceType>(serviceResponse.Data);
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

        public int SaveOccurrenceType(OccurrenceType item) {
            var serviceMethodName = "SaveOccurrenceType";
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = item;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var id = JsonConvert.DeserializeObject<int>(serviceResponse.Data);
                return id;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public int CopyOccurrenceType(int occurrenceTypeId, int productId, int coverageId, int loggedUserId) {
            var serviceMethodName = "SaveOccurrenceType";
            var methodParameters = new { occurrenceTypeId, productId, coverageId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                OccurrenceType item = GetOccurrenceType(occurrenceTypeId);
                item.OccurrenceTypeId = null;
                item.ProductId = productId;
                item.CoverageId = coverageId;
                item.LoggedUserId = loggedUserId;

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = item;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var id = JsonConvert.DeserializeObject<int>(serviceResponse.Data);
                return id;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public void DeleteOccurrenceType(int occurrenceTypeId, int loggedUserId) {
            var serviceMethodName = "DeleteOccurrenceType";
            var methodParameters = new { occurrenceTypeId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    occurrenceTypeId,
                    loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }


        public IList<PolicyBatchConfiguration> ListPolicyBatchConfiguration(PolicyBatchConfigurationFilters filters) {
            var serviceMethodName = "ListPolicyBatchConfiguration";
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
                var items = JsonConvert.DeserializeObject<IList<PolicyBatchConfiguration>>(serviceResponse.Data);
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

        public PolicyBatchConfiguration GetPolicyBatchConfiguration(int policyBatchConfigurationId) {
            var serviceMethodName = "GetPolicyBatchConfiguration";
            var methodParameters = new { policyBatchConfigurationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyBatchConfigurationId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<PolicyBatchConfiguration>(serviceResponse.Data);
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

        public int SavePolicyBatchConfiguration(PolicyBatchConfiguration item) {
            var serviceMethodName = "SavePolicyBatchConfiguration";
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = item;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var id = JsonConvert.DeserializeObject<int>(serviceResponse.Data);
                return id;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public void DeletePolicyBatchConfiguration(int policyBatchConfigurationId, int loggedUserId) {
            var serviceMethodName = "DeletePolicyBatchConfiguration";
            var methodParameters = new { policyBatchConfigurationId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    policyBatchConfigurationId,
                    loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public IList<PolicyBatchConfigurationMail> ListPolicyBatchConfigurationMails(int policyBatchConfigurationId) {
            var serviceMethodName = "ListPolicyBatchConfigurationMails";
            var methodParameters = new { policyBatchConfigurationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyBatchConfigurationId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<PolicyBatchConfigurationMail>>(serviceResponse.Data);
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

        public PolicyBatchConfigurationMail GetPolicyBatchConfigurationMail(int policyBatchConfigurationMailId) {
            var serviceMethodName = "GetPolicyBatchConfigurationMail";
            var methodParameters = new { policyBatchConfigurationMailId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { policyBatchConfigurationMailId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<PolicyBatchConfigurationMail>(serviceResponse.Data);
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

        public int SavePolicyBatchConfigurationMail(PolicyBatchConfigurationMail item) {
            var serviceMethodName = "SavePolicyBatchConfigurationMail";
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = item;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var id = JsonConvert.DeserializeObject<int>(serviceResponse.Data);
                return id;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public void DeletePolicyBatchConfigurationMail(int policyBatchConfigurationMailId, int loggedUserId) {
            var serviceMethodName = "DeletePolicyBatchConfigurationMail";
            var methodParameters = new { policyBatchConfigurationMailId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    policyBatchConfigurationMailId,
                    loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }

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
