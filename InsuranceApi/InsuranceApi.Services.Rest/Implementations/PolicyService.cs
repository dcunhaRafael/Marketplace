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
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Implementations {
    public class PolicyService : IPolicyService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public PolicyService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<ApoliceRetornoEmitirEntity> IssuePolicyAsync(ApoliceAssinadaEmitirEntity policy, int brokerUserId) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Policy).BaseUrl + AppConfigHttp.RotaApoliceEmitir;
                var response = await url
                  .PostJsonAsync(PolicyMapper.MapApoliceEmitirEntityToRequest(policy, brokerUserId))
                  .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                  .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<ApoliceEmitirResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return PolicyMapper.MapApoliceEmitirResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Apolice_Emitir).", e);
            }
        }

        public async Task<BilletPrintEntity> GetBilletPrintAsync(int endorsementId) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Policy)
                 .Request(AppConfigHttp.RotaApoliceImprimirBoleto)
                 .SetQueryParam("id_endosso", endorsementId)
                 .GetJsonAsync<ApoliceImprimirBoletoResponse>()
                 .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno);
                }

                return PolicyMapper.MapApoliceImprimirBoletoResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (ObterPdfBoleto).", e);
            }
        }

        public async Task<PolicyPrintEntity> GetPolicyPrintAsync(int endorsementId) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Policy)
                 .Request(AppConfigHttp.RotaApoliceImprimir)
                 .SetQueryParam("id_endosso", endorsementId)
                 .GetJsonAsync<ApoliceImprimirResponse>()
                 .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno);
                }

                return PolicyMapper.MapApoliceImprimirResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Apolice_Imprimir).", e);
            }
        }

        public async Task<List<PolicyReturnEntity>> ListPolicyAsync(PolicySearchEntity filters) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Policy).BaseUrl + AppConfigHttp.RotaApolicePesquisar;
                var response = await url
                  .PostJsonAsync(PolicyMapper.MapApolicePesquisarEntityToRequest(filters))
                  .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                  .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<ApolicePesquisarResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return PolicyMapper.MapApolicePesquisarResponseToEntity(entity).OrderByDescending(x => Convert.ToInt64(x.NumeroProposta)).ToList();

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Apolice_Pesquisar).", e);
            }
        }
    }
}
