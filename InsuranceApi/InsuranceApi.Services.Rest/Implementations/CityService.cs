using Flurl.Http;
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
    public class CityService : ICityService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public CityService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<CidadeConsultarEntity>> ListAsync(int ufCode, string cityName) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.City).BaseUrl + AppConfigHttp.RotaConsultaCidade;
                var response = await url
                              .PostJsonAsync(CityMapper.MapCidadeConsultarEntityToRequest(ufCode, cityName))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<CidadeConsultarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    if (entity.nm_retorno.Contains("Não existe"))
                        entity.nm_retorno = entity.nm_retorno.Replace(".", " Cidade.");
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return CityMapper.MapCidadeConsultarResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Consulta_Cidade).", e);
            }
        }
    }
}
