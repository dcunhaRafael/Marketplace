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

namespace InsuranceApi.Web.Controllers {

    [Authorize]
    [Route("api/tiporecurso")]
    public class LegalRecourseTypeController : BaseController {
        private readonly ILegalRecourseTypeApplication legalRecourseTypeApplication;
        private readonly IMapper mapper;

        public LegalRecourseTypeController(IUser user, ILogger<LegalRecourseTypeController> logger, IMapper mapper,
                                           IAuditApplication auditApplication, ILegalRecourseTypeApplication legalRecourseTypeApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.legalRecourseTypeApplication = legalRecourseTypeApplication;
        }

        [HttpPost("tiporecurso_buscar")]
        public async Task<ActionResult> ListLegalRecourseTypeAsync() {
            try {

                base.WriteAuditData(LogLevel.Trace, "Listar Tipos de Recursos", null, null);

                var recursoEntity = await legalRecourseTypeApplication.ListAsync();
                if (recursoEntity == null || recursoEntity.Count == 0) return NotFound();

                var items = mapper.Map<List<LegalRecourseTypeReturnViewMdoel>>(recursoEntity);
                base.WriteAuditData(LogLevel.Debug, "Listar Tipos de Recursos", null, items);

                return base.ReturnSuccess(items);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Listar Tipos de Recursos", null, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}