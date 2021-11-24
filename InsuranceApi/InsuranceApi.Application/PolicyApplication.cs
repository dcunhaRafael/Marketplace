using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class PolicyApplication : IPolicyApplication {
        private readonly IPolicyService policyService;
        private readonly IProposalService propostaService;
        private readonly IBrokerApplication brokerApplication;
        private readonly IDigitalSignatureApplication digitalSignatureApplication;
        private readonly IAppParameterApplication appParameterApplication;
        private readonly IProposalDao proposalDao;
        private readonly IProposalSignatureDao proposalSignatureDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalDao proposalMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalStatusDao proposalStatusMpDao;

        public PolicyApplication(IPolicyService policyService, IProposalService propostaService, IBrokerApplication brokerApplication,
            IDigitalSignatureApplication digitalSignatureApplication, IAppParameterApplication appParameterApplication, IProposalDao proposalDao,
            IProposalSignatureDao proposalSignatureDao, InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalDao proposalMpDao,
            InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalStatusDao proposalStatusMpDao) {
            this.policyService = policyService;
            this.propostaService = propostaService;
            this.brokerApplication = brokerApplication;
            this.digitalSignatureApplication = digitalSignatureApplication;
            this.proposalDao = proposalDao;
            this.proposalSignatureDao = proposalSignatureDao;
            this.appParameterApplication = appParameterApplication;
            this.proposalMpDao = proposalMpDao;
            this.proposalStatusMpDao = proposalStatusMpDao;
        }

        public async Task<ApoliceRetornoEmitirEntity> IssueAsync(IssueProposalEntity issueData, int? proposalBrokerUserId) {
            if (string.IsNullOrEmpty(issueData.PropostaAssinada.IP)) {
                throw new Exception("Não foi possível encontrar o IP na requisição");
            }
            if (string.IsNullOrEmpty(issueData.PropostaAssinada.UserAgent)) {
                throw new Exception("Não foi possível encontrar o user-agent na requisição");
            }
            CorretorConsultaEntity brokerAccessProposal = await brokerApplication.GetAsync(null, proposalBrokerUserId);
            if (brokerAccessProposal == null) {
                throw new Exception("Não foi possível encontrar o corretor associado ao usuário na requisição");
            }
            var proposta = await propostaService.GetAsync(issueData.CodigoProposta, brokerAccessProposal.IdUsuarioCorretor.Value, brokerAccessProposal.CodigoSusep);
            if (proposta == null) {
                throw new Exception("Não foi possível encontrar a proposta");
            }

            var proposalSignature = await proposalSignatureDao.GetAsync(proposta.CodigoProposta);
            if (proposalSignature == null) {
                var pdf = await propostaService.PrintAsync(proposta.IdEndosso);
                var OrigemEmpresa = await appParameterApplication.GetAsync(AppParameterEnum.OrigemEmpresaAssinatura);


                var assinaturaDigital = new AssinaturaGravarEntity() {
                    CpfCnpj = Convert.ToInt64(brokerAccessProposal.CpfCnpj),
                    Nome = brokerAccessProposal.NomePessoa,
                    CodigoProposta = proposta.CodigoProposta,
                    Documento = new DocumentEntity() {
                        NomeArquivo = pdf.NomeArquivo,
                        ConteudoBase64 = pdf.Base64
                    },
                    IpAddress = issueData.PropostaAssinada.IP,
                    UserAgent = issueData.PropostaAssinada.UserAgent,
                    OrigemEmpresa = OrigemEmpresa.Value
                };

                var assinaturaDigitalRetorno = await digitalSignatureApplication.AddAsync(assinaturaDigital);
                var assinaturaDigitalConsulta = await digitalSignatureApplication.GetAsync(assinaturaDigitalRetorno.IdTransacao);
                var documentoAssinado = await digitalSignatureApplication.PrintAsync(assinaturaDigitalConsulta.ImpressoAssinado.Href);

                proposalSignature = new ProposalAssinaturaEntity() {
                    CodigoProposta = proposta.CodigoProposta,
                    DataAssinatura = assinaturaDigitalConsulta.TimestampInclusao,
                    DocumentoAssinatura = documentoAssinado.ConteudoBase64,
                    NomeDocumentoAssinatura = pdf.NomeArquivo,
                    IdTransacao = assinaturaDigitalRetorno.IdTransacao
                };
                try {
                    await proposalSignatureDao.AddAsync(proposalSignature);
                } catch (Exception e) {
                    // Erros são ignorados já que o usuário nada tem a ver com isso
                    // Se deu erro na gravação da proposta aqui vai dar erro porque não vai ter o registro pai
                    //Log.Error("ApoliceApplication.EmitirApolice", e);
                }
            }

            var apoliceAssinada = new ApoliceAssinadaEmitirEntity() {
                IdApolice = proposta.IdApolice,
                IdEndosso = proposta.IdEndosso,
                IndicadorPropostaAssinada = true,
                DataAssinaturaProposta = proposalSignature.DataAssinatura,
                ObservacoesAssinatura = "Apolice Assinada"
            };

            var policyIssued = await policyService.IssuePolicyAsync(apoliceAssinada, brokerAccessProposal.IdUsuarioCorretor.Value);

            var propostaAtualizada = await propostaService.GetAsync(issueData.CodigoProposta, brokerAccessProposal.IdUsuarioCorretor.Value, brokerAccessProposal.CodigoSusep);
            policyIssued.StatusProposta = propostaAtualizada.StatusProposta.Value;

            await proposalDao.UpdateStatusAsync(proposta.CodigoProposta, (int)policyIssued.StatusProposta);

            //-- Atualiza DB marketplace
            var proposalStatus = await proposalStatusMpDao.GetAsync((int)policyIssued.StatusProposta);
            if (proposalStatus == null) {
                throw new Exception("Não foi possível identificar o status da proposta no Marketplace.");
            }
            await proposalMpDao.UpdatePolicyAsync(issueData.CodigoProposta, policyIssued.NumeroApolice, proposalStatus);

            return policyIssued;
        }

        public async Task<ApoliceRetornoEmitirEntity> IssueSignedAsync(int proposalCode, int transactionId, int? proposalBrokerUserId) {
            CorretorConsultaEntity brokerAccessProposal = await brokerApplication.GetAsync(null, proposalBrokerUserId);
            if (brokerAccessProposal == null) {
                throw new Exception("Corretor associado ao usuário não pode ser localizado");
            }

            var proposta = await propostaService.GetAsync(proposalCode, brokerAccessProposal.IdUsuarioCorretor.Value, brokerAccessProposal.CodigoSusep);
            if (proposta == null) {
                throw new Exception("Proposta não localizada.");
            }

            var proposalSignature = await proposalSignatureDao.GetAsync(proposta.CodigoProposta);
            if (proposalSignature == null) {
                var assinaturaDigitalConsulta = await digitalSignatureApplication.GetAsync(transactionId, brokerAccessProposal.CpfCnpj);
                var documentoAssinado = await digitalSignatureApplication.PrintAsync(assinaturaDigitalConsulta);
                proposalSignature = new ProposalAssinaturaEntity() {
                    CodigoProposta = proposta.CodigoProposta,
                    DataAssinatura = DateTime.Now,
                    DocumentoAssinatura = documentoAssinado.ConteudoBase64,
                    NomeDocumentoAssinatura = documentoAssinado.NomeArquivo,
                    IdTransacao = transactionId
                };
                try {
                    await proposalSignatureDao.AddAsync(proposalSignature);
                } catch (Exception e) {
                }
            }

            var apoliceAssinada = new ApoliceAssinadaEmitirEntity() {
                IdApolice = proposta.IdApolice,
                IdEndosso = proposta.IdEndosso,
                IndicadorPropostaAssinada = true,
                DataAssinaturaProposta = proposalSignature.DataAssinatura,
                ObservacoesAssinatura = "Apolice Assinada"
            };

            var policyIssued = await policyService.IssuePolicyAsync(apoliceAssinada, brokerAccessProposal.IdUsuarioCorretor.Value);

            var propostaAtualizada = await propostaService.GetAsync(proposalCode, brokerAccessProposal.IdUsuarioCorretor.Value, brokerAccessProposal.CodigoSusep);

            policyIssued.StatusProposta = propostaAtualizada.StatusProposta.Value;

            await proposalDao.UpdateStatusAsync(proposta.CodigoProposta, (int)policyIssued.StatusProposta);


            return policyIssued;
        }

        public async Task<BilletPrintEntity> PrintBilletAsync(int endorsementCode) {
            return await policyService.GetBilletPrintAsync(endorsementCode);
        }

        public async Task<PolicyPrintEntity> PrintAsync(int endorsementCode) {
            return await policyService.GetPolicyPrintAsync(endorsementCode);
        }

        public async Task<List<PolicyReturnEntity>> ListAsync(PolicySearchEntity filters) {
            var broker = await brokerApplication.GetAsync(null, filters.IdUsuario.Value);
            if (broker != null) {
                filters.CodigoSusepUsuario = broker.CodigoSusep;
            }
            return await policyService.ListPolicyAsync(filters);
        }
    }
}
