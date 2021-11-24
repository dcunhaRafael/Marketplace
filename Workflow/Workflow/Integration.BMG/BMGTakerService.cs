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
    public class BMGTakerService : BaseLogger, ILegacyTakerService {
        private readonly RestClient client;
        private const string TAKER_SERVICE_NAME = "ServicoTomador/";

        public BMGTakerService(ILogger<BMGTakerService> logger, IOptions<EndpointSettings> endpointSettings) : base(logger) {
            var i4proServiceEndpoint = endpointSettings?.Value?.I4pro;
            if (string.IsNullOrWhiteSpace(i4proServiceEndpoint)) {
                throw new IntegrationException("Endpoint para acesso aos serviços do sistema de back office não parametrizado.");
            }
            client = new RestClient(i4proServiceEndpoint);
        }

        public void Dispose() {
            GC.KeepAlive(client);
        }

        public TakerData Get(string takerLegacyCode, int brokerUserId, LoggerComplement loggerComplement) {
            var takers = List(takerLegacyCode, null, null, brokerUserId, true, true, true, true, loggerComplement);
            return takers?.FirstOrDefault();
        }

        public IList<TakerData> List(string takerLegacyCode, string name, long? cpfCnpjNumber, int brokerUserId, bool isActive, bool defaultAddress, bool listBroker, bool listTaker, LoggerComplement loggerComplement) {
            var serviceName = "Tomador_Buscar";
            var methodParameters = new { takerLegacyCode, name, cpfCnpjNumber, brokerUserId, isActive, defaultAddress, listBroker, listTaker };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa_tomador = takerLegacyCode,
                    dv_ativo = isActive,
                    dv_endereco_padrao = defaultAddress,
                    //dv_situacao = null,
                    //dv_ccg_formalizado = null,
                    //dv_dominio_tomador = null,
                    //dv_dominio_corretor = null,
                    dv_lista_corretor = listBroker,
                    nm_pessoa = name,
                    nr_cnpj_cpf = cpfCnpjNumber == null ? null : cpfCnpjNumber?.FormatCpfCnpj().KeepNumbersOnly(),
                    id_usuario = brokerUserId,
                    dv_lista_tomador = listTaker
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<TomadorBuscarResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    if (response.cd_retorno != 1) { // "Não existe dados para os parâmetros informado."
                        throw new LegacyServiceException(response.nm_retorno);
                    }
                }
                var items = TakerMapper.Map(response);
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

        public TakerCreditLimit GetCreditLimit(string takerId, LoggerComplement loggerComplement) {
            var serviceName = "Tomador_BuscarLimitesCredito";
            var methodParameters = new { takerId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, loggerComplement);

            RawRequest rawRequest = new();
            try {

                rawRequest.RequestUri = string.Format("{0}{1}", TAKER_SERVICE_NAME, serviceName);
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", loggerComplement);

                rawRequest.BodyObject = new {
                    id_pessoa = takerId
                };
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, loggerComplement);

                RawResponse rawResponse = client.Post<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, loggerComplement);

                var response = JsonConvert.DeserializeObject<TomadorBuscarLimitesCreditoResponse>(rawResponse.Conteudo);
                if (response.cd_retorno != 0) {
                    throw new LegacyServiceException(response.nm_retorno);
                }
                var item = TakerMapper.Map(response);
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
