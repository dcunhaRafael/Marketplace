using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.HttpClients;
using Domain.Util.Log;
using Integration.BMG.Mappers;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Integration.BMG {
    public class RenewalApiService : BaseLogger, IRenewalApiService {
        private readonly RestClient client;
        private const string RENEWAL_SERVICE_NAME = "renewal/";

        public RenewalApiService(ILogger<RenewalApiService> logger, IOptions<EndpointSettings> endpointSettings) : base(logger) {
            var renewalApiEndpoint = endpointSettings?.Value?.RenewalApi;
            if (string.IsNullOrWhiteSpace(renewalApiEndpoint)) {
                throw new IntegrationException("Endpoint para acesso aos serviços da API de renovação de seguros não parametrizado.");
            }
            client = new RestClient(renewalApiEndpoint);
        }

        public void Dispose() {
            GC.KeepAlive(client);
        }

        public PolicyRenovation SaveRenewalJudicial(PolicyRenovation model) {
            var serviceName = "gravar_renovacao_judicial";
            var methodParameters = new { model };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}", RENEWAL_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = new {
                    PolicyCode = model.PolicyCode,
                    BrokerDocument = model.BrokerDocument,
                    NewInsuredAmount = model.NewInsuredAmount,
                    NewStartOfTerm = model.NewStartOfTerm,
                    NewEndOfTerm = model.NewEndOfTerm,
                    NewInsuredObject = model.NewInsuredObject
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<ProposalAdded>(rawResponse.Conteudo);
                var item = RenewalApiMapper.Map(response);
                return item;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public ProposalTransmited TransmitProposal(PolicyRenovation model) {
            var serviceName = "transmitir_proposta";
            var methodParameters = new { model };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}", RENEWAL_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = new {
                    NumeroProposta = model.NewProposalNumber
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<ProposalTransmited>(rawResponse.Conteudo);
                return response;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public PolicyIssued IssuePolicy(PolicyRenovation model) {
            var serviceName = "emitir_apolice";
            var methodParameters = new { model };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}", RENEWAL_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = new {
                    NumeroProposta = model.NewProposalNumber,
                    IsPropostaAssinada = false, //TODO: fixar?
                    IdTransacao = (int?)null    //TODO: fixar?
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<PolicyIssued>(rawResponse.Conteudo);
                return response;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public ProposalPrinted PrintProposal(PolicyRenovation model) {
            var serviceName = "imprimir_minuta";
            var methodParameters = new { model };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}", RENEWAL_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = new {
                    CodigoEndosso = model.EndorsementId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<ProposalPrinted>(rawResponse.Conteudo);
                return response;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public ProposalPrinted PrintPolicy(PolicyRenovation model) {
            var serviceName = "imprimir_apolice";
            var methodParameters = new { model };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}", RENEWAL_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = new {
                    CodigoEndosso = model.EndorsementId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<ProposalPrinted>(rawResponse.Conteudo);
                return response;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

    }
}
