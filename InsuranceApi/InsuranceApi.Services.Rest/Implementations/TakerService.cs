using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Services.Rest.Mappers;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Services.Rest.Implementations {
    public class TakerService : ITakerService {
        private readonly IHttpServiceClientFactory httpRestClient;
        private readonly ILogger log;

        public TakerService(ILoggerFactory log, IHttpServiceClientFactory httpRestClient) {
            this.log = log.CreateLogger("TakerService");
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<TakerModel>> ListAsync(string name, string cpfCnpj, int? brokerUserId, bool listAll = false) {
            TomadorBuscarEntity filtros = new TomadorBuscarEntity {
                NomePessoa = name,
                CpfCnpj = cpfCnpj,
                ListaCorretor = true,
                ListarTodos = listAll
            };

            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Taker).BaseUrl + AppConfigHttp.RotaTomadorBuscar;
                var response = await url
                              .PostJsonAsync(TakerMapper.MapTomadorBuscarEntityToRequest(filtros, brokerUserId))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<TomadorBuscarResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    if (!entity.nm_retorno.Contains("Não existe dados para os parâmetros informados.")) {
                        throw new ServiceException("Tomador não localizado na i4pro.");
                    }
                    throw new InvalidCastException(entity.nm_retorno, entity.cd_retorno);
                }

                return TakerMapper.MapTomadorBuscarResponseToEntity(entity);

            } catch (InvalidCastException e) {
                log.LogError(e.InnerException, e.Message + $"{ cpfCnpj}");
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Tomador_Buscar).", e);
            }
        }

        public async Task<TomadorRetornoIncluirEntity> AddAsync(TakerModel taker, int? brokerUserId) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Taker).BaseUrl + AppConfigHttp.RotaTomadorIncluir;
                var response = await url
                              .PostJsonAsync(TakerMapper.MapTomadorIncluirEntityToRequest(taker, brokerUserId))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<TomadorIncluirResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return TakerMapper.MapTomadorIncluirResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (TomadorIncluir).", e);
            }
        }

        public async Task<TakerCalculationParameters> GetParametersAsync(int takerExternalCode) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Taker)
                             .Request(AppConfigHttp.RotaTomadorBuscarParametrosCalculo)
                             .SetQueryParam("id_pessoa", takerExternalCode)
                             .GetJsonAsync<TomadorBuscarParametrosCalculoResponse>()
                             .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno, response.cd_retorno);
                }

                return TakerMapper.MapTomadorBuscarParametrosCalculoResponseToEntity(response); ;

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Tomador_BuscarParametrosCalculo).", e);
            }
        }
    }
}
