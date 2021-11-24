using AutoMapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RenewalApi.Web.ViewModel;
using RenewalApi.Web.ViewModels;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RenewalApi.Web.Controllers {
    [ApiController]
    [Route("api/renewal")]
    public class RenewalController : BaseController {
        private readonly IMapper mapper;
        private readonly IProposalApplication propostaApplication;
        private readonly IPolicyApplication policyApplication;

        public RenewalController(IUser user, ILogger<RenewalController> logger, IMapper mapper, IAuditApplication auditApplication, 
            IProposalApplication propostaApplication, IPolicyApplication policyApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.propostaApplication = propostaApplication;
            this.policyApplication = policyApplication;
        }

        [HttpPost]
        [Route("gravar_renovacao_judicial")]
        public async Task<ActionResult> SaveRenewalJudicialAsync(RenewalJudicialViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Gravar Proposta Renovação Judicial", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEntity = await propostaApplication.AddRenovalAsync(mapper.Map<ProposalRenewalEntity>(model));
                var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity.Proposta);
                base.WriteAuditData(LogLevel.Debug, "Gravar Proposta Renovação Judicial", model, proposta);

                return base.ReturnSuccess(proposta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Gravar Proposta Renovação Judicial", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost]
        [Route("transmitir_proposta")]
        public async Task<ActionResult> TransmitProposalAsync(TransmitProposalViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Verificar Aprovação Proposta", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEmitirEntity = await propostaApplication.ApproveAsync(model.NumeroProposta, base.UsuarioExternalId);
                var responseData = new {
                    CodigoStatus = propostaEmitirEntity.StatusProposta.ToString("D"),
                    DescricaoStatus = propostaEmitirEntity.StatusProposta.ToDescription()
                };
                base.WriteAuditData(LogLevel.Debug, "Verificar Aprovação Proposta", model, responseData);

                return base.ReturnSuccess(responseData);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Verificar Aprovação Proposta", model, e);
                return base.ReturnErrorAndLog(e, System.Reflection.MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("emitir_apolice")]
        public async Task<ActionResult> IssuePolicyAsync(PolicyIssueViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Emitir Apólice", model, null);

                if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState);

                ApoliceRetornoEmitirEntity retornoEmissao = new ApoliceRetornoEmitirEntity();

                if (model.IsPropostaAssinada) {
                    retornoEmissao = await policyApplication.IssueSignedAsync(model.NumeroProposta, model.IdTransacao.Value, base.UsuarioExternalId);
                } else {
                    var policyEntity = new IssueProposalEntity() {
                        CodigoProposta = model.NumeroProposta,
                        PropostaAssinada = new ProposalSignatureEntity() {
                            IP = HttpContext.Connection.LocalIpAddress.ToString(),
                            UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                        }
                    };
                    retornoEmissao = await policyApplication.IssueAsync(policyEntity, base.UsuarioExternalId);
                }

                var responseData = new {
                    retornoEmissao.NumeroApolice,
                    CodigoStatus = retornoEmissao.StatusProposta.ToString("D"),
                    DescricaoStatus = retornoEmissao.StatusProposta.ToDescription()
                };
                base.WriteAuditData(LogLevel.Debug, "Emitir Apólice", model, responseData);

                return base.ReturnSuccess(responseData);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Emitir Apólice", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("imprimir_minuta")]
        public async Task<ActionResult> PrintProposalAsync(PrintViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Imprimir Minuta Proposta", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var minuta = await propostaApplication.PrintAsync(model.CodigoEndosso);

                var responseData = new {
                    NomeArquivo = $"Minuta-{model.CodigoEndosso}.pdf",
                    minuta.Base64
                };
                base.WriteAuditData(LogLevel.Debug, "Imprimir Minuta Proposta", model, minuta);

                return base.ReturnSuccess(responseData);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Obter Minuta Proposta", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("imprimir_apolice")]
        public async Task<ActionResult> PrintPolicyAsync(PrintViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Imprimir Apólice", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var minuta = await policyApplication.PrintAsync(model.CodigoEndosso);

                var responseData = new {
                    NomeArquivo = $"Apólice-{model.CodigoEndosso}.pdf",
                    minuta.Base64
                };
                base.WriteAuditData(LogLevel.Debug, "Imprimir Apólice", model, minuta);

                return base.ReturnSuccess(responseData);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Imprimir Apólice", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}
