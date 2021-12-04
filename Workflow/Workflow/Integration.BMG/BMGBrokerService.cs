using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Extensions;
using Domain.Util.HttpClients;
using Domain.Util.Log;
using Integration.BMG.Mappers;
using Integration.BMG.Schemas;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Integration.BMG {
    public class BMGBrokerService : BaseLogger, ILegacyBrokerService {
        private readonly RestClient client;
        private const string TAKER_SERVICE_NAME = "ServicoCorretor/";

        public BMGBrokerService(ILogger<BMGBrokerService> logger, IOptions<EndpointSettings> endpointSettings) : base(logger) {
            var i4proServiceEndpoint = endpointSettings?.Value?.I4pro;
            if (string.IsNullOrWhiteSpace(i4proServiceEndpoint)) {
                throw new IntegrationException("Endpoint para acesso aos serviços do sistema de back office não parametrizado.");
            }
            client = new RestClient(i4proServiceEndpoint);
        }

        public void Dispose() {
            GC.KeepAlive(client);
        }

        public List<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement) {
            var serviceName = "Corretor_Extrato_Comissao";
            var methodParameters = new { statementNumber, fromDate, toDate, status, brokerLegacyCode, brokerSusepCode, brokerUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_corretor = brokerLegacyCode,
                    cd_susep = brokerSusepCode,
                    cd_status = status,
                    dt_periodo_inicial = fromDate,
                    dt_periodo_final = toDate,
                    cd_extrato = statementNumber,
                    cd_usuario_autenticacao = brokerUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<CorretorExtratoComissaoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    if (response.cd_retorno != 1) { // "Não existe dados para os parâmetros informado."
                        throw new LegacyServiceException(response.nm_retorno);
                    }
                }
                var items = BrokerMapper.Map(response);
                return items;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, loggerComplement);
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", loggerComplement);
            }
        }

        public List<ComissionStatementDetail> ListComissionStatementDetails(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement) {
            var serviceName = "Corretor_Detalhe_Extrato_Comissao";
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_corretor = brokerLegacyCode,
                    cd_susep = brokerSusepCode,
                    cd_extrato = statementNumber,
                    dt_competencia = competency?.FormatCompetency(),
                    cd_usuario_autenticacao = brokerUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<CorretorDetalheExtratoComissaoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    throw new LegacyServiceException(response.nm_retorno);
                }
                var item = BrokerMapper.Map(response);
                return item;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, loggerComplement);
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", loggerComplement);
            }
        }

        public List<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement) {
            var serviceName = "Corretor_Extrato_Tipo_Comissao";
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_corretor = brokerLegacyCode,
                    cd_susep = brokerSusepCode,
                    cd_extrato = statementNumber,
                    dt_competencia = competency?.FormatCompetency(),
                    cd_usuario_autenticacao = brokerUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<CorretorExtratoTipoComissaoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    throw new LegacyServiceException(response.nm_retorno);
                }
                var item = BrokerMapper.Map(response);
                return item;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, loggerComplement);
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", loggerComplement);
            }
        }

        public List<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement) {
            var serviceName = "Consulta_Extrato_Comissao_Por_Ramo";
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_corretor = brokerLegacyCode,
                    cd_susep = brokerSusepCode,
                    cd_extrato = statementNumber,
                    dt_competencia = competency?.FormatCompetency(),
                    cd_usuario_autenticacao = brokerUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<CorretorExtratoComissaoPorRamoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    throw new LegacyServiceException(response.nm_retorno);
                }
                var item = BrokerMapper.Map(response);
                return item;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, loggerComplement);
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", loggerComplement);
            }
        }

        public List<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement) {
            var serviceName = "Consulta_Lancamento_Extrato_Comissao";
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_corretor = brokerLegacyCode,
                    cd_susep = brokerSusepCode,
                    cd_extrato = statementNumber,
                    dt_competencia = competency?.FormatCompetency(),
                    cd_usuario_autenticacao = brokerUserId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<CorretorLancamentoExtratoComissaoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    throw new LegacyServiceException(response.nm_retorno);
                }
                var item = BrokerMapper.Map(response);
                return item;

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, loggerComplement);
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", loggerComplement);
            }
        }

    }
}
