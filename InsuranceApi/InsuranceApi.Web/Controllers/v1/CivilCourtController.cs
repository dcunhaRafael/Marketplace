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
    [Route("api/vara")]
    public class CivilCourtController : BaseController {
        private readonly IMapper mapper;
        private readonly ICivilCourtApplication civilCourtApplication;

        public CivilCourtController(IUser user, ILogger<CivilCourtController> logger, IMapper mapper,
                                    IAuditApplication auditApplication, ICivilCourtApplication civilCourtApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.civilCourtApplication = civilCourtApplication;
        }

        [HttpPost("vara_buscar")]
        public async Task<ActionResult> ListCivilCourtAsync(CivilCourtViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Listar Varas", model, null);

                if (!ModelState.IsValid) {
                    return base.ReturnErrorAndLogModelState(ModelState);
                }

                var civilCourtEntity = await civilCourtApplication.ListAsync(model.CodigoTribunal, model.Nome);
                if (civilCourtEntity == null || civilCourtEntity.Count == 0) return NotFound();

                var items = mapper.Map<List<CivilCourtReturnViewModel>>(civilCourtEntity);
                base.WriteAuditData(LogLevel.Debug, "Listar Varas", model, items);

                return base.ReturnSuccess(items);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Listar Varas", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}

