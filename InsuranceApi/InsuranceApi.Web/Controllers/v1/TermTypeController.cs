using AutoMapper;
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

namespace InsuranceApi.Web.Controllers.v1 {

    [Authorize]
    [Route("api/prazo")]
    public class TermTypeController : BaseController {
        private readonly IMapper mapper;
        private readonly ITermTypeApplication termTypeApplication;

        public TermTypeController(IUser user, ILogger<TermTypeController> logger, IMapper mapper,
                                  IAuditApplication auditApplication, ITermTypeApplication termTypeApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.termTypeApplication = termTypeApplication;
        }

        [HttpPost("prazo_buscar")]
        public async Task<ActionResult> ListTermTypeAsync(TermTypeViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Listar Prazos", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var prazoEntity = await termTypeApplication.ListAsync(model.CodigoModalidade);
                if (prazoEntity == null || prazoEntity.Count == 0) return NotFound();

                var items = mapper.Map<List<TermTypeReturnViewModel>>(prazoEntity);
                base.WriteAuditData(LogLevel.Debug, "Listar Prazos", model, items);

                return base.ReturnSuccess(items);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Listar Prazos", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}
