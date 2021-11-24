using Flurl.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Services.Rest.Mappers;
using InsuranceApi.Services.Rest.Messages.Legacy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InsuranceApi.Services.Rest.Implementations {
    public class ProposalSignatureService : ISignatureDigitalService {
        private readonly ILogger log;
        private readonly IHttpServiceClientFactory httpRestClient;

        public ProposalSignatureService(ILoggerFactory log, IHttpServiceClientFactory httpRestClient) {
            this.log = log.CreateLogger("ProposalSignatureService");
            this.httpRestClient = httpRestClient;
        }

        public async Task<AssinaturaRetornoGravarEntity> AddAsync(AssinaturaGravarEntity signature) {
            try {
                
                var url = httpRestClient.GetService(ServiceClientTypeEnum.SignatureDigital).BaseUrl + AppConfigHttp.RotaAssinaturaGravar + "/" + signature.OrigemEmpresa;
                var response = await url
                  .PostJsonAsync(SignatureDigitalMapper.MapAssinaturaGravarEntityToRequest(signature))                  
                  .ConfigureAwait(false);

                var entity = SignatureDigitalMapper.MapAssinaturaGravarResponseToEntity(response);

                if (entity.Erros != null && entity.Erros.Count > 0) {
                    throw new InvalidCastException("Erro na gravação da assinatura digital: " + entity.Erros.Aggregate((i, j) => i + "; " + j));
                }

                return entity;
            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                log.LogError(e.InnerException, e.Message + $"{  JsonConvert.SerializeObject(SignatureDigitalMapper.MapAssinaturaGravarEntityToRequest(signature)) }");
                throw new ServiceException("Erro na comunicação com o serviço de assinatura (AssinaturaDigital_Gravar).", e, SignatureDigitalMapper.MapAssinaturaGravarEntityToRequest(signature));
            }
        }

        public async Task<AssinaturaConsultarEntity> GetAsync(int transactionId, string companyOrigin) {
            try {
                var response = await httpRestClient.GetService(ServiceClientTypeEnum.SignatureDigital)
                       .Request(string.Format(AppConfigHttp.RotaAssinaturaConsultar, transactionId, companyOrigin))
                       .GetJsonAsync<AssinaturaConsultarResponse>()
                       .ConfigureAwait(false);

                return SignatureDigitalMapper.MapAssinaturaConsultarResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com o serviço de assinatura (AssinaturaDigital_Consultar).", e);
            }
        }

        public async Task<string> GetAsync(int transactionId, string insurerCpfCnpj, string brokerCpfcnpj, string signatureType) {
            try {
                var response = await httpRestClient.GetService(ServiceClientTypeEnum.ProposalSignatureDigital)
                       .Request(string.Format(AppConfigHttp.RotaAssinaturaConsultar, transactionId, insurerCpfCnpj))
                       .SetQueryParam("corretora", brokerCpfcnpj)
                       .SetQueryParam("tipo", signatureType)
                       .GetJsonAsync<PropostaAssinadaConsultarEntity>()
                       .ConfigureAwait(false);

                return response.Documento.FirstOrDefault().ImpressoAssinado;

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com o serviço de assinatura (AssinaturaDigital_Consultar).", e);
            }
        }

        public async Task<AssinaturaImpressoEntity> PrintAsync(string url) {
            try {

                var queryString = string.Format(AppConfigHttp.RotaImpressaoAssinatura, FormatosEnum.json);
                var requestUri = string.Format("{0}{1}", url, queryString);

                var response = await requestUri.GetJsonAsync<AssinaturaImpressaoResponse>().ConfigureAwait(false);

                return SignatureDigitalMapper.MapAssinaturaImpressaoResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com o serviço de assinatura (AssinaturaDigital_Imprimir).", e);
            }
        }

        public async Task<AssinaturaTomadorRetornoEntity> AddAsync(AssinaturaTomadorEntity signature) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.SignatureDigital).BaseUrl + AppConfigHttp.RotaAssinaturaGravar + "/" + signature.OrigemEmpresa;
                var response = await url
                  .PostJsonAsync(SignatureDigitalMapper.MapAssinaturaGravarTomadorEntityToRequest(signature))
                  .ConfigureAwait(false);

                var entity = SignatureDigitalMapper.MapAssinaturaGravarTomadorResponseToEntity(response);

                if (entity.Erros != null && entity.Erros.Count > 0) {
                    throw new InvalidCastException("Erro na gravação da assinatura digital: " + entity.Erros.Aggregate((i, j) => i + "; " + j));
                }

                return entity;
            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                log.LogError(e.InnerException, e.Message + $"{  JsonConvert.SerializeObject(SignatureDigitalMapper.MapAssinaturaGravarTomadorEntityToRequest(signature)) }");
                throw new ServiceException("Erro na comunicação com o serviço de assinatura (AssinaturaDigital_Gravar).", e, SignatureDigitalMapper.MapAssinaturaGravarTomadorEntityToRequest(signature));
            }
        }
    }
}

