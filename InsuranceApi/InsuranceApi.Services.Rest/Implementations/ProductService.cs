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
    public class ProductService :  IProductService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public ProductService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<ProductEntity>> ListAsync(int brokerUserId) {
            try {
                
                var url = httpRestClient.GetService(ServiceClientTypeEnum.Product).BaseUrl + AppConfigHttp.RotaProdutoBuscar;                
                var response = await url
                              .PostJsonAsync(new ProdutoBuscarRequest { id_usuario = brokerUserId, dv_habilitado_emissao_portal = true })
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<ProdutoBuscarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }
                return ProductMapper.MapProdutoBuscarResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Produto_Buscar).", e);
            }
        }
    }
}
