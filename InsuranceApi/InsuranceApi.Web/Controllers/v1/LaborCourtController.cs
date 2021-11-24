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
    [Route("api/tribunal")]
    public class LaborCourtController : BaseController {
        private readonly IMapper mapper;
        private readonly ILaborCourtApplication laborCourtApplication;

        public LaborCourtController(IUser user, ILogger<TermTypeController> logger, IMapper mapper,
                                    IAuditApplication auditApplication, ILaborCourtApplication laborCourtApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.laborCourtApplication = laborCourtApplication;
        }
        
        [HttpPost("tribunal_buscar")]
        public async Task<ActionResult> ListLaborCourtAsync(LaborCourtViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Listar Tribunais", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var tribunalEntity = await laborCourtApplication.ListAsync(model.Nome);
                if (tribunalEntity == null || tribunalEntity.Count == 0) return NotFound();

                var items = mapper.Map<List<LaborCourtReturnViewModel>>(tribunalEntity);
                base.WriteAuditData(LogLevel.Debug, "Listar Tribunais", model, items);

                return base.ReturnSuccess(items);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Listar Tribunais", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}

