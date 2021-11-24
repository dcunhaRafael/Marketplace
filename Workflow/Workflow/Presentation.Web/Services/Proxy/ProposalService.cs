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
    public class ProposalService : BaseLogger, IProposalService {
        private readonly IServiceFactory serviceFactory;
        private const string SERVICE_NAME = "proposal/";

        public ProposalService(ILogger<ProposalService> logger, IServiceFactory serviceFactory) : base(logger) {
            this.serviceFactory = serviceFactory;
        }

        public void Dispose() {
            GC.KeepAlive(serviceFactory);
        }

        public IList<ProposalOccurrence> ListOccurrences(ProposalOccurrenceFilters filters) {
            var serviceMethodName = "ListOccurrences";
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
                var items = JsonConvert.DeserializeObject<IList<ProposalOccurrence>>(serviceResponse.Data);
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

        public ProposalOccurrence GetOccurrence(long proposalOccurrenceId) {
            var serviceMethodName = "GetOccurrence";
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<ProposalOccurrence>(serviceResponse.Data);
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

        public IList<User> ListOccurrenceLiberationUsers(long proposalOccurrenceId) {
            var serviceMethodName = "ListOccurrenceLiberationUsers";
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<User>>(serviceResponse.Data);
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

        public IList<ProposalOccurrenceHistory> ListOccurrenceHistories(long proposalOccurrenceId) {
            var serviceMethodName = "ListOccurrenceHistories";
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ProposalOccurrenceHistory>>(serviceResponse.Data);
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

        public void ForwardOccurrence(ProposalOccurrenceForward item) {
            var serviceMethodName = "ForwardOccurrence";
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

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public IList<ProposalOccurrenceDocument> ListOccurrenceDocuments(long proposalOccurrenceId) {
            var serviceMethodName = "ListOccurrenceDocuments";
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var items = JsonConvert.DeserializeObject<IList<ProposalOccurrenceDocument>>(serviceResponse.Data);
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

        public long AddOccurrenceDocument(ProposalOccurrenceDocument item, int loggedUserId) {
            var serviceMethodName = "AddOccurrenceDocument";
            var methodParameters = new { item, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new {
                    DocumentTypeId = item.DocumentTypeId,
                    FileName = item.FileName,
                    FileContentsBase64 = Convert.ToBase64String(item.FileContents),
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    LoggedUserId = loggedUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                return Convert.ToInt64(serviceResponse.Data);

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public ProposalOccurrenceDocument GetOccurrenceDocument(long proposalOccurrenceDocumentId) {
            var serviceMethodName = "GetOccurrenceDocument";
            var methodParameters = new { proposalOccurrenceDocumentId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceDocumentId };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());
                RawResponse rawResponse = serviceFactory.ServiceClient.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var serviceResponse = JsonConvert.DeserializeObject<ServiceReturn>(rawResponse.Conteudo);
                if (!serviceResponse.Success) {
                    throw new LegacyServiceException(serviceResponse.Message);
                }
                var item = JsonConvert.DeserializeObject<ProposalOccurrenceDocument>(serviceResponse.Data);
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

        public void DeleteOccurrenceDocument(long proposalOccurrenceDocumentId, int loggedUserId) {
            var serviceMethodName = "DeleteOccurrenceDocument";
            var methodParameters = new { proposalOccurrenceDocumentId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new RawRequest();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", SERVICE_NAME, serviceMethodName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());
                rawRequest.BodyObject = new { proposalOccurrenceDocumentId, loggedUserId };
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

        public void ApproveOccurrence(ProposalOccurrenceApprove item) {
            var serviceMethodName = "ApproveOccurrence";
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

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceMethodName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public void RefuseOccurrence(ProposalOccurrenceRefuse item) {
            var serviceMethodName = "RefuseOccurrence";
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
