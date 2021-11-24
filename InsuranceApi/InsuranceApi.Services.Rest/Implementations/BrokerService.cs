﻿using Flurl.Http;
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
    public class BrokerService : IBrokerService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public BrokerService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<BrokerEntity> GetAsync(int proposalBrokerUserId) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Broker)
                    .Request(AppConfigHttp.RotaCorretorBuscar)
                    .SetQueryParam("id_usuario", proposalBrokerUserId)
                    .SetQueryParam("dv_ativo", true)
                    .GetJsonAsync<CorretorBuscarResponse>()
                    .ConfigureAwait(false);

                if (response == null)
                    throw new BusinessException("Não foi possível obter Broker associado ao usuário");

                return BrokerMapper.MapCorretorBuscarResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Corretor_Buscar).", e);
            }
        }

        public async Task<IList<CorretorConsultaEntity>> ListAsync(CorretorConsultaEntity filters) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Broker).BaseUrl + AppConfigHttp.RotaConsultaCorretor;
                var response = await url
                  .PostJsonAsync(BrokerMapper.MapCorretorConsultaEntityToRequest(filters))
                  .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                  .ConfigureAwait(false);
                var entity = JsonConvert.DeserializeObject<CorretorConsultaResponse>(response.Content.ReadAsStringAsync().Result);
                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }
                return BrokerMapper.MapCorretorConsultaEntityToRequest(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Corretor_Buscar).", e);
            }
        }
    }
}
