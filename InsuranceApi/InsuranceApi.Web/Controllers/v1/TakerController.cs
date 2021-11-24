using AutoMapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers {

    [Authorize]
    [Route("api/tomador")]
    public class TakerController : BaseController {
        private readonly ITakerApplication takerApplication;
        private readonly IMapper mapper;

        public TakerController(IUser user, ILogger<TakerController> logger, IMapper mapper,
                               IAuditApplication auditApplication, ITakerApplication takerApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.takerApplication = takerApplication;
        }

        [HttpPost("tomador_buscar")]
        public async Task<ActionResult> ListTakerAsync(TakerSearchViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Buscar Tomador", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var taker = mapper.Map<List<TakerReturnViewModel>>(await takerApplication.ListAsync("", Extends.ApenasNumericos(model.CpfCnpj), base.UsuarioExternalId));
                base.WriteAuditData(LogLevel.Debug, "Buscar Tomador", model, taker);

                return base.ReturnSuccess(taker);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Buscar Tomador", model, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }


        [HttpPost]
        [Route("tomador_inserir")]
        public async Task<ActionResult> AddTakerAsync(InsertTakerViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Inserir Tomador", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                //-- IMPORTANTE
                //Como a tabela de usuários da API Ebix usa GUID e nâo INT então precisa fixar usuário,
                //e pena análise dos registros sempre gravava 14 q é o usuário Ebix, então o valor é passado fixo
                //-- IMPORTANTE

                var taker = await takerApplication.AddAsync(mapper.Map<TakerModel>(model), base.UsuarioExternalId,
                                                            HttpContext.Connection.LocalIpAddress.ToString(), 
                                                            HttpContext.Request.Headers["User-Agent"].ToString(), 
                                                            AppConfigHttp.CodigoUsuarioPadraoEbix);  

                base.WriteAuditData(LogLevel.Debug, "Inserir Tomador", model, taker);

                return base.ReturnSuccess(null);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Inserir Tomador", model, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}