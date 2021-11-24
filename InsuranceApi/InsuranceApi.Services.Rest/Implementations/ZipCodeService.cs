using Flurl.Http;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Services.Rest.Mappers;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Threading.Tasks;

namespace InsuranceApi.Services.Rest.Implementations {
    public class ZipCodeService : IZipCodeService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public ZipCodeService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<ZipCodeEntity> GetAsync(string zipCode) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Search)
                             .Request(AppConfigHttp.RotaCepBuscar)
                             .SetQueryParam("nm_cep", zipCode)
                             .GetJsonAsync<CepBuscarResponse>()
                             .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    if (response.nm_retorno.Contains("Não existe"))
                        response.nm_retorno = response.nm_retorno.Replace(".", " Cep.");
                    throw new InvalidCastException(response.nm_retorno);
                }

                return ZipCodeMapper.MapCepBuscarResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Cep_Buscar).", e);
            }
        }
    }
}
