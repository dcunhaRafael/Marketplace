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

namespace InsuranceApi.Services.Rest.Implementations {
    public class ProposalService : IProposalService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public ProposalService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<ProposalReturnGravarEntity> AddAsync(ProposalEntity proposal, int brokerUserId, string brokerSusepCode) {
            try {

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Proposal).BaseUrl + AppConfigHttp.RotaPropostaGravar;

                var response = await url
                            .PostJsonAsync(ProposalMapper.MapPropostaGravarEntityToRequest(proposal, brokerUserId, brokerSusepCode))
                            .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                            .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<PropostaGravarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }
                return ProposalMapper.MapPropostaGravarResponseToEntity(entity, proposal);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Proposta_Gravar).", e);
            }
        }

        public async Task<PropostaRetornoVerificarAprovacaoEntity> CheckApprovalAsync(int proposalCode, int brokerUserId, string brokerSusepCode) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.ProposalTransmission)
                              .Request(AppConfigHttp.RotaPropostaVerificarAprovacao)
                              .SetQueryParam("cd_proposta", proposalCode)
                              .SetQueryParam("cd_susep", brokerSusepCode)
                              .SetQueryParam("id_usuario", brokerUserId)
                              .GetJsonAsync<PropostaVerificarAprovacaoResponse>()
                              .ConfigureAwait(false);

                return ProposalMapper.MapPropostaVerificarAprovacaoResponseToEntity(response);

            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Proposta_VerificarAprovacao).", e);
            }
        }

        public async Task<ProposalEntity> GetAsync(int proposalCode, int brokerUserId, string brokerSusepCode) {
            try {

                // Monta a estrutura de filtros padrão para a consulta geral
                PropostaFiltrosEntity filtros = new PropostaFiltrosEntity
                {
                    CodigoProposta = proposalCode,
                    Corretor = new CorretorConsultaEntity
                    {
                        IdUsuarioCorretor = brokerUserId,
                        CodigoSusep = brokerSusepCode
                    }
                };

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Proposal).BaseUrl + AppConfigHttp.RotaPropostaPesquisar;
                var response = await url
                              .PostJsonAsync(ProposalMapper.MapPropostaBuscarEntityToRequest(filtros, brokerUserId, brokerSusepCode))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<PropostaPesquisarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return ProposalMapper.MapPropostaObterResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Proposta_Buscar).", e);
            }
        }

        public async Task<ProposalEntity> GetAsync(long policyNumber, int brokerUserId, string brokerSusepCode) {
            try {

                // Monta a estrutura de filtros padrão para a consulta geral
                PropostaFiltrosEntity filtros = new PropostaFiltrosEntity {
                    IdApolice = policyNumber,
                    Corretor = new CorretorConsultaEntity {
                        IdUsuarioCorretor = brokerUserId,
                        CodigoSusep = brokerSusepCode
                    }
                };

                var url = httpRestClient.GetService(ServiceClientTypeEnum.Proposal).BaseUrl + AppConfigHttp.RotaPropostaPesquisar;
                var response = await url
                              .PostJsonAsync(ProposalMapper.MapPropostaBuscarEntityToRequest(filtros, brokerUserId, brokerSusepCode))
                              .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                              .ConfigureAwait(false);

                var entity = JsonConvert.DeserializeObject<PropostaPesquisarResponse>(response.Content.ReadAsStringAsync().Result);

                if (entity.cd_retorno != 0) {
                    throw new InvalidCastException(entity.nm_retorno);
                }

                return ProposalMapper.MapPropostaObterResponseToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Proposta_Buscar).", e);
            }
        }

        public async Task<ProposalReturnAprovarEntity> ApproveAsync(int proposalCode, int brokerUserId, string brokerSusepCode) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Proposal)
                             .Request(AppConfigHttp.RotaPropostaAprovar)
                             .SetQueryParam("cd_proposta", proposalCode)
                             .SetQueryParam("cd_susep", brokerUserId)
                             .SetQueryParam("id_usuario", brokerSusepCode)
                             .GetJsonAsync<PropostaVerificarAprovacaoResponse>()
                             .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno);
                }

                return ProposalMapper.MapPropostaAprovarResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Proposta_Aprovar).", e);
            }
        }

        public async Task<PropostaRetornoImprimirEntity> PrintAsync(int endorsementId) {
            try {

                var response = await httpRestClient.GetService(ServiceClientTypeEnum.Proposal)
                            .Request(AppConfigHttp.RotaPropostaImprimirMinuta)
                            .SetQueryParam("id_endosso", endorsementId)
                            .GetJsonAsync<PropostaImprimirMinutaResponse>()
                            .ConfigureAwait(false);

                if (response.cd_retorno != 0) {
                    throw new InvalidCastException(response.nm_retorno);
                }

                return ProposalMapper.MapPropostaImprimirResponseToEntity(response);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a i4pro (Imprimir_Minuta).", e);
            }
        }
    }
}
