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
    public class ComissionStatementService : BaseLogger, IComissionStatementService {
        private readonly IServiceFactory serviceFactory;
        private const string SERVICE_NAME = "comissionStatement/";

        public ComissionStatementService(ILogger<CommonService> logger, IServiceFactory serviceFactory) : base(logger) {
            this.serviceFactory = serviceFactory;
        }

        public void Dispose() {
            GC.KeepAlive(serviceFactory);
        }

        public IList<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, Broker broker, int loggedUserId) {
            var serviceMethodName = "ListComissionStatement";
            var methodParameters = new { statementNumber, fromDate, toDate, status, broker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    StatementNumber = statementNumber,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Status = status,
                    BrokerLegacyCode = broker.LegacyCode,
                    BrokerSusepCode = broker.SusepCode,
                    BrokerUserId = broker.LegacyUserId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ComissionStatement>>(serviceResponse.Data);
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

        public IList<ComissionStatementDetail> ListComissionStatementDetais(int statementNumber, string competency, Broker broker, int loggedUserId) {
            var serviceMethodName = "ListComissionStatementDetais";
            var methodParameters = new { statementNumber, competency, broker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    StatementNumber = statementNumber,
                    Competency = competency,
                    BrokerLegacyCode = broker.LegacyCode,
                    BrokerSusepCode = broker.SusepCode,
                    BrokerUserId = broker.LegacyUserId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ComissionStatementDetail>>(serviceResponse.Data);
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

        public IList<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, Broker broker, int loggedUserId) {
            var serviceMethodName = "ListComissionStatementTypes";
            var methodParameters = new { statementNumber, competency, broker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    StatementNumber = statementNumber,
                    Competency = competency,
                    BrokerLegacyCode = broker.LegacyCode,
                    BrokerSusepCode = broker.SusepCode,
                    BrokerUserId = broker.LegacyUserId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ComissionStatementType>>(serviceResponse.Data);
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

        public IList<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, Broker broker, int loggedUserId) {
            var serviceMethodName = "ListComissionStatementBusiness";
            var methodParameters = new { statementNumber, competency, broker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    StatementNumber = statementNumber,
                    Competency = competency,
                    BrokerLegacyCode = broker.LegacyCode,
                    BrokerSusepCode = broker.SusepCode,
                    BrokerUserId = broker.LegacyUserId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ComissionStatementBusiness>>(serviceResponse.Data);
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

        public IList<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, Broker broker, int loggedUserId) {
            var serviceMethodName = "ListComissionStatementEntries";
            var methodParameters = new { statementNumber, competency, broker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    StatementNumber = statementNumber,
                    Competency = competency,
                    BrokerLegacyCode = broker.LegacyCode,
                    BrokerSusepCode = broker.SusepCode,
                    BrokerUserId = broker.LegacyUserId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ComissionStatementEntry>>(serviceResponse.Data);
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

    }
}
