using Flurl.Http;
using Newtonsoft.Json;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Services.Rest.Mappers;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace InsuranceApi.Services.Rest.Implementations {
    public class InsuredService : IInsuredService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public InsuredService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<InsuredEntity>> ListAsync(SeguradoBuscarEntity filters) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Insured).BaseUrl + AppConfigHttp.RotaSeguradoPesquisar;
                var response = await url
                              .PostJsonAsync(InsuredMapper.MapSeguradoBuscarEntityToRequest(filters))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);


                var entity = JsonConvert.DeserializeObject<SeguradoPesquisarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return InsuredMapper.MapSeguradoBuscarResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Segurado_Buscar).", e);
            }
        }

        public async Task<SeguradoRetornoIncluirEntity> AddAsync(InsuredEntity insured) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Insured).BaseUrl + AppConfigHttp.RotaSeguradoIncluir;
                var response = await url
                              .PostJsonAsync(InsuredMapper.MapSeguradoIncluirEntityToRequest(insured))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<SeguradoIncluirResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return InsuredMapper.MapSeguradoIncluirResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Segurado_Incluir).", e);
            }
        }
    }
}
