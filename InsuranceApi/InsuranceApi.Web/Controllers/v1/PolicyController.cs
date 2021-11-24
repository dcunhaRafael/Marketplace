using AutoMapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers.v1 {

    [Authorize]
    [Route("api/apolice")]
    public class PolicyController : BaseController {
        private readonly IPolicyApplication policyApplication;
        private readonly IMapper mapper;

        public PolicyController(IUser user, ILogger<PolicyController> logger, IMapper mapper,
                                IAuditApplication auditApplication, IPolicyApplication policyApplication) : base(user, logger, auditApplication) {
            this.policyApplication = policyApplication;
            this.mapper = mapper;
        }

        [HttpPost("apolice_emitir")]
        public async Task<ActionResult> IssuePolicyAsync(IssueProposalViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Emitir Apólice", model, null);

                if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState);

                ApoliceRetornoEmitirEntity retornoEmissao = new ApoliceRetornoEmitirEntity();

                if (model.IsPropostaAssinada) {
                    retornoEmissao = await policyApplication.IssueSignedAsync(model.CodigoProposta, model.IdTransacao.Value, base.UsuarioExternalId);
                } else {
                    var policyEntity = new IssueProposalEntity() {
                        CodigoProposta = model.CodigoProposta,
                        PropostaAssinada = new ProposalSignatureEntity() {
                            IP = HttpContext.Connection.LocalIpAddress.ToString(),
                            UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                        }
                    };
                    retornoEmissao = await policyApplication.IssueAsync(policyEntity, base.UsuarioExternalId);
                }
                base.WriteAuditData(LogLevel.Debug, "Emitir Apólice", model, retornoEmissao);

                return base.ReturnSuccess(retornoEmissao);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Emitir Apólice", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("apolice_imprimir")]
        public async Task<ActionResult> PrintPolicyAsync(BilletViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Imprimir Apólice", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var minuta = await policyApplication.PrintAsync(model.CodigoEndosso);
                base.WriteAuditData(LogLevel.Debug, "Imprimir Apólice", model, minuta);

                return base.ReturnSuccess(minuta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Imprimir Apólice", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }


        [HttpPost("apolice_pesquisar")]
        public async Task<ActionResult> ListPolicyAsync(PolicySearchViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Pesquisar Apólice", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var filtro = new PolicySearchEntity() {
                    NumeroProposta = model.NumeroProposta,
                    NumeroApolice = model.NumeroApolice,
                    DataInicial = model.DataInicial,
                    DataFinal = model.DataFinal,
                    IdUsuario = base.UsuarioExternalId
                };
                var policies = await policyApplication.ListAsync(filtro);
                base.WriteAuditData(LogLevel.Debug, "Pesquisar Apólice", model, policies);

                return base.ReturnSuccess(policies);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Pesquisar Apólice", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}
