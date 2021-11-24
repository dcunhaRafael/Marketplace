using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers.v1 {

    [Authorize]
    [Route("api/segurado")]
    public class InsuredController : BaseController {
        private readonly IInsuredApplication insuredApplication;                

        public InsuredController(IUser user, ILogger<PolicyController> logger,
                                 IAuditApplication auditApplication, IInsuredApplication insuredApplication) : base(user, logger, auditApplication) {
            this.insuredApplication = insuredApplication;            
        }

        [HttpPost("segurado_buscar")]
        public async Task<ActionResult> GetInsuredAsync(InsuredSearchViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Buscar Segurado", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var insured = await insuredApplication.GetAsync(Extends.ApenasNumericos(model.CpfCnpj));
                base.WriteAuditData(LogLevel.Debug, "Buscar Segurado", model, insured);

                return base.ReturnSuccess(insured);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Buscar Segurado", model, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}
