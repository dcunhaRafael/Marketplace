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
    public class BacenService : BaseLogger, IBacenService {
        private const string TAKER_SERVICE_NAME = "ServicoTomador/";
        private readonly string bacenEndpoint;

        public BacenService(ILogger<BacenService> logger, IOptions<EndpointSettings> endpointSettings) : base(logger) {
            bacenEndpoint = endpointSettings?.Value?.Bacen;
            if (string.IsNullOrWhiteSpace(bacenEndpoint)) {
                throw new IntegrationException("Endpoint para acesso aos serviços do sistema do Bacen não parametrizado.");
            }
        }

        public void Dispose() {
            //GC.KeepAlive(client);
        }

        public IList<SelicTax> ListDaily(DateTime start, DateTime finish) {
            RestClient client = new RestClient(string.Format(bacenEndpoint, 11));//TODO

            var serviceName = "dados";
            var methodParameters = new { start, finish };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}?formato=json&dataInicial={2}&dataFinal={3}", "", serviceName, start.FormatDate(), finish.FormatDate());
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = null;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Get<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<SelicTaxResponse[]>(rawResponse.Conteudo);
                var items = BacenMapper.Map(response);
                return items.OrderBy(x=> x.Date).ToList();

            } catch (Exception e) {
                if (!(e is LegacyServiceException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new IntegrationException($"Erro na chamada do serviço '{serviceName}': {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", new LoggerComplement());
            }
        }

        public IList<SelicTax> ListMonthly(DateTime start, DateTime finish) {
            RestClient client = new RestClient(string.Format(bacenEndpoint, 4390));//TODO

            var serviceName = "dados";
            var methodParameters = new { start, finish };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, new LoggerComplement());

            RawRequest rawRequest = new();
            try {

                rawRequest.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50");
                rawRequest.RequestUri = string.Format("{0}{1}?formato=json&dataInicial={2}&dataFinal={3}", "", serviceName, start.FormatDate(), finish.FormatDate());
                LogDebug(MethodBase.GetCurrentMethod(), $"URI: {rawRequest.RequestUri}", new LoggerComplement());

                rawRequest.BodyObject = null;
                LogDebug(MethodBase.GetCurrentMethod(), "Body", rawRequest.BodyObject, new LoggerComplement());

                RawResponse rawResponse = client.Get<RawRequest, RawResponse>(rawRequest.RequestUri, rawRequest);
                LogDebug(MethodBase.GetCurrentMethod(), "RawResponse", rawResponse, new LoggerComplement());

                var response = JsonConvert.DeserializeObject<SelicTaxResponse[]>(rawResponse.Conteudo);
                var items = BacenMapper.Map(response);
                return items.OrderBy(x => x.Date).ToList();

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
