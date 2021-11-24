using AutoMapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModel;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers {

    [Authorize]
    [Route("api/proposta")]
    public class ProposalController : BaseController {
        private readonly IMapper mapper;
        private readonly IProposalAppealApplication proposalAppealApplication;
        private readonly IProposalBiddingApplication proposalBiddingApplication;
        private readonly IProposalContractApplication proposalContractApplication;
        private readonly IProposalJudicialApplication proposalJudicialApplication;
        private readonly IProposalApplication propostaApplication;

        public ProposalController(IUser user, ILogger<ProposalController> logger, IMapper mapper,
                                  IAuditApplication auditApplication, IProposalApplication propostaApplication, IProposalContractApplication proposalContractApplication,
                                  IProposalBiddingApplication proposalBiddingApplication, IProposalAppealApplication proposalAppealApplication,
                                  IProposalJudicialApplication proposalJudicialApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.propostaApplication = propostaApplication;
            this.proposalContractApplication = proposalContractApplication;
            this.proposalAppealApplication = proposalAppealApplication;
            this.proposalBiddingApplication = proposalBiddingApplication;
            this.proposalJudicialApplication = proposalJudicialApplication;
        }

        [HttpPost]
        [Route("proposta_gravar_recursal")]
        public async Task<ActionResult> AddProposalAppealAsync(ProposalAppealViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Gravar Proposta Recursal", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEntity = await proposalAppealApplication.AddAsync(mapper.Map<ProposalAppealEntity>(model), base.UsuarioExternalId);
                var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity.Proposta);
                base.WriteAuditData(LogLevel.Debug, "Gravar Proposta Recursal", model, proposta);

                return base.ReturnSuccess(proposta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Gravar Proposta Recursal", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost]
        [Route("proposta_gravar_licitacao")]
        public async Task<ActionResult> AddProposalBiddingAsync(ProposalBiddingViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Gravar Proposta Licitação", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEntity = await proposalBiddingApplication.AddAsync(mapper.Map<ProposalBiddingEntity>(model), base.UsuarioExternalId);
                var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity.Proposta);
                base.WriteAuditData(LogLevel.Debug, "Gravar Proposta Licitação", model, proposta);

                return base.ReturnSuccess(proposta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Gravar Proposta Licitação", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost]
        [Route("proposta_gravar_contrato")]
        public async Task<ActionResult> AddProposalConntractAsync(ProposalContractViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Gravar Proposta Contrato", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEntity = await proposalContractApplication.AddAsync(mapper.Map<ProposalContractEntity>(model), base.UsuarioExternalId);
                var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity.Proposta);
                base.WriteAuditData(LogLevel.Debug, "Gravar Proposta Contrato", model, proposta);

                return base.ReturnSuccess(proposta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Gravar Proposta Contrato", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }

        //[HttpPost]
        //[Route("proposta_gravar_judicial")]
        //public async Task<ActionResult> AddProposalJudicialAsync(ProposalJudicialViewModel model) {
        //    try {

        //        base.WriteAuditData(LogLevel.Trace, "Gravar Proposta Judicial", model, null);

        //        if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

        //        var propostaEntity = await proposalJudicialApplication.AddAsync(mapper.Map<ProposalJudicialEntity>(model), base.UsuarioExternalId);
        //        var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity.Proposta);
        //        base.WriteAuditData(LogLevel.Debug, "Gravar Proposta Judicial", model, proposta);

        //        return base.ReturnSuccess(proposta);

        //    } catch (Exception e) {
        //        base.WriteAuditData(LogLevel.Error, "Gravar Proposta Judicial", model, e);
        //        return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
        //    }
        //}

        [HttpPost]
        [Route("verificar_aprovacao")]
        public async Task<ActionResult> ApproveProposalAsync(ApproveProposalViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Verificar Aprovação Proposta", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEmitirEntity = await propostaApplication.ApproveAsync(model.CodigoProposta, base.UsuarioExternalId);
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

        [HttpPost("obter_proposta")]
        public async Task<ActionResult> GetProposalAsync(SearchProposalViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Obter Proposta", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var propostaEntity = await propostaApplication.GetAsync(model.CodigoProposta, base.UsuarioExternalId);
                var proposta = mapper.Map<ProposalRetornoViewModel>(propostaEntity);
                base.WriteAuditData(LogLevel.Debug, "Obter Proposta", model, proposta);

                return base.ReturnSuccess(proposta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Obter Proposta", model, e);
                return base.ReturnErrorAndLog(e, System.Reflection.MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("obter_minuta")]
        public async Task<ActionResult> PrintProposalAsync(ProposalMinutaViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Obter Minuta Proposta", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var minuta = await propostaApplication.PrintAsync(model.CodigoEndosso);
                base.WriteAuditData(LogLevel.Debug, "Obter Minuta Proposta", model, minuta);

                return base.ReturnSuccess(minuta);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Obter Minuta Proposta", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}