using Flurl.Http;
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
    public class CoverageService : ICoverageService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public CoverageService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<CoverageEntity>> ListAsync(int productExternalCode, int brokerUserId) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Coverage)
                   .Request(AppConfigHttp.RotaCoberturaBuscar)
                   .SetQueryParam("cd_produto", productExternalCode)
                   .SetQueryParam("id_usuario", brokerUserId)
                   .SetQueryParam("id_pessoa", 0)
                   .SetQueryParam("cd_susep", "")
                   .GetJsonAsync<CoberturaBuscarResponse>()
                   .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno);
                }
                var items = CoverageMapper.MapCoberturaBuscarResponseToEntity(response);

                return items;

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Cobertura_Buscar).", e);
            }
        }
    }
}
